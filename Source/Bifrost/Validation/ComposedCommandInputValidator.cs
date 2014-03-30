using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Commands;
using Bifrost.Extensions;
using FluentValidation;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a command business validator that is constructed from discovered rules.
    /// </summary>
    public class ComposedCommandInputValidator<T> : InputValidator<T>, ICanValidate<T>, ICommandInputValidator where T : class, ICommand
    {
        /// <summary>
        /// Instantiates an Instance of a <see cref="ComposedCommandInputValidator{T}"/>
        /// </summary>
        /// <param name="propertyTypesAndValidators">A collection of dynamically discovered validators to use</param>
        public ComposedCommandInputValidator(IDictionary<Type,IEnumerable<IValidator>> propertyTypesAndValidators)
        {
            var validatorDescriptorType = typeof(ValidatorDescriptor<>).MakeGenericType(typeof(T));
            var validatorsField = typeof(AbstractValidator<T>).GetField("nestedValidators", BindingFlags.NonPublic | BindingFlags.Instance);
            IEnumerable<IValidationRule> validationRules;
            if (validatorsField != null)
                validationRules = validatorsField.GetValue(this) as IEnumerable<IValidationRule>;
            else 
                validationRules = new IValidationRule[0];

            var dynamicValidationRuleMethod = typeof(FluentValidationRuleExtensions).GetMethod("DynamicValidationRule", BindingFlags.Static | BindingFlags.Public);

            foreach (var propertyType in propertyTypesAndValidators.Keys)
            {
                var validators = propertyTypesAndValidators[propertyType];

                if (validators == null || !validators.Any()) 
                    continue;
                
                var properties = GetPropertiesWithType(propertyType);
                foreach (var property in properties)
                {
                    //var expression = BuildGetExpression(property);

                    var buildGetExpressionMethod = GetType().GetMethod("BuildGetExpression", BindingFlags.NonPublic | BindingFlags.Instance);
                    var buildGetExpression = buildGetExpressionMethod.MakeGenericMethod(propertyType);

                    var expression = buildGetExpression.Invoke(this, new[]{property});

                    var getValidatorMethod = GetType().GetMethod("GetValidator", BindingFlags.NonPublic | BindingFlags.Instance);
                    var getValidator = getValidatorMethod.MakeGenericMethod(propertyType);
                    var validator = getValidator.Invoke(this, new object[] { property.Name, validators }) as IComposedValidator;

                    //var validator = GetValidator(property.Name, validators);

                    var rulesBefore = new List<IValidationRule>(validationRules);

                    var ruleForMethod = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Single(m => m.Name == "RuleFor" && m.ContainsGenericParameters);
                    var ruleFor = ruleForMethod.MakeGenericMethod(propertyType);


                    var ruleBuilder = ruleFor.Invoke(this, new[] {expression});
                        //RuleFor(expression);
                    var dynamicValidationRule = dynamicValidationRuleMethod.MakeGenericMethod(typeof(T), propertyType);
                    dynamicValidationRule.Invoke(null, new object[] {
                        ruleBuilder,
                        validator, 
                        property.Name
                    });
                    
                        //.DynamicValidationRule(validator, property.Name);

                    var newRules = validationRules.Except(rulesBefore);
                    validator.SetRules(newRules);
                }
            }
        }


        IValidator GetValidator<TP>(string propertyName, IEnumerable<IValidator> propertyTypesAndValidator)
        {
            return new ComposedValidator<TP>(propertyName, propertyTypesAndValidator);
        }

        IEnumerable<PropertyInfo> GetPropertiesWithType(Type propertyType)
        {
            var commandType = typeof (T);
            var properties = commandType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == propertyType);
            return properties;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return ValidateFor(command);
        }

        public virtual IEnumerable<ValidationResult> ValidateFor(T command)
        {
            var result = Validate(command as T);
            return from error in result.Errors
                   select new ValidationResult(error.ErrorMessage, new[] { error.PropertyName });
        }

        IEnumerable<ValidationResult> ICanValidate.ValidateFor(object target)
        {
            return ValidateFor((T)target);
        }
#pragma warning restore 1591 // Xml Comments

        Expression<Func<T, TP>> BuildGetExpression<TP>(PropertyInfo propertyInfo)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            expr = Expression.Property(expr, propertyInfo);

            return Expression.Lambda<Func<T, TP>>(expr, arg);
        }
    }
}