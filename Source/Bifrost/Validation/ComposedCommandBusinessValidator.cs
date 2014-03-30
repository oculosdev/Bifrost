using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Bifrost.Commands;
using System.Linq;
using FluentValidation;
using Bifrost.Extensions;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a command business validator that is constructed from discovered rules.
    /// </summary>
    public class ComposedCommandBusinessValidator<T> : BusinessValidator<T>, ICanValidate<T>, ICommandInputValidator where T : class, ICommand
    {
        /// <summary>
        /// Instantiates an Instance of a <see cref="ComposedCommandBusinessValidator{T}"/>
        /// </summary>
        /// <param name="propertyTypesAndValidators">A collection of dynamically discovered validators to use</param>
        public ComposedCommandBusinessValidator(IDictionary<Type, IEnumerable<IValidator>> propertyTypesAndValidators)
        {
            var validatorDescriptorType = typeof(ValidatorDescriptor<>).MakeGenericType(typeof(T));
            var validatorsField = typeof(AbstractValidator<T>).GetField("nestedValidators", BindingFlags.NonPublic | BindingFlags.Instance);
            IEnumerable<IValidationRule> validationRules;
            if (validatorsField != null)
                validationRules = validatorsField.GetValue(this) as IEnumerable<IValidationRule>;
            else
                validationRules = new IValidationRule[0];

            foreach (var propertyType in propertyTypesAndValidators.Keys)
            {
                var validators = propertyTypesAndValidators[propertyType];

                if (validators == null || !validators.Any()) 
                    continue;

                var properties = GetPropertiesWithType(propertyType);
                foreach (var property in properties)
                {
                    var expression = BuildGetExpression(property);
                    var validator = GetValidator(property.Name, validators);

                    var rulesBefore = new List<IValidationRule>(validationRules);

                    RuleFor(expression)
                        .DynamicValidationRule(validator, property.Name);

                    var newRules = validationRules.Except(rulesBefore);
                    validator.SetRules(newRules);
                }
            }
        }

        ComposedValidator<IAmValidatable> GetValidator(string propertyName, IEnumerable<IValidator> propertyTypesAndValidator)
        {
            return new ComposedValidator<IAmValidatable>(propertyName, propertyTypesAndValidator);
        }

        IEnumerable<PropertyInfo> GetPropertiesWithType(Type propertyType)
        {
            var commandType = typeof(T);
            var properties = commandType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == propertyType);
            return properties;
        }

#pragma warning disable 1591 // Xml Comments
        public IEnumerable<ValidationResult> ValidateFor(ICommand command)
        {
            return ValidateFor(command as T);
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

        static Expression<Func<T, IAmValidatable>> BuildGetExpression(PropertyInfo propertyInfo)
        {
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;

            expr = Expression.Property(expr, propertyInfo);

            return Expression.Lambda<Func<T, IAmValidatable>>(expr, arg);
        }
    }
}