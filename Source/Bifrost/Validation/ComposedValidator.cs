using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace Bifrost.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComposedValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules"></param>
        void SetRules(IEnumerable<IValidationRule> rules);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComposedValidatorDescriptor<T> : IValidatorDescriptor
    {
        string _propertyName;
        IEnumerable<IValidationRule> _rules;
        IEnumerable<IValidator> _validators;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules"></param>
        /// <param name="validators"></param>
        /// <param name="propertyName"></param>
        public ComposedValidatorDescriptor(IEnumerable<IValidationRule> rules, IEnumerable<IValidator> validators, string propertyName)
        {
            _rules = rules;
            _validators = validators;
            _propertyName = propertyName;
        }

#pragma warning disable 1591 // Xml Comments
        public ILookup<string, IPropertyValidator> GetMembersWithValidators()
        {
            var query = from rule in _rules.OfType<PropertyRule>()
                        where rule.PropertyName != null
                        from validator in rule.Validators
                        select new { propertyName = rule.PropertyName, validator };

            return query.ToLookup(x => _propertyName, x => x.validator);
        }

        public string GetName(string property)
        {
            return _propertyName;
        }

        public IEnumerable<IValidationRule> GetRulesForMember(string name)
        {
            return _rules;
        }

        public IEnumerable<IPropertyValidator> GetValidatorsForMember(string name)
        {
            return from rule in _rules.OfType<PropertyRule>()
                   where Equals(rule.Member, name)
                   from validator in rule.Validators
                   select validator;
        }
#pragma warning restore 1591 // Xml Comments
    }
    

    /// <summary>
    /// Combines multiples validators into a single validator
    /// </summary>
    /// <typeparam name="T">Type that the composite validator validates</typeparam>
    public class ComposedValidator<T> : AbstractValidator<T>, IComposedValidator
    {
        readonly List<IValidator> registeredValidators = new List<IValidator>();
        ComposedValidatorDescriptor<T> _descriptor;
        string _propertyName;


        /// <summary>
        /// Instantiates an instance of a <see cref="ComposedValidator{T}"/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="validators"></param>
        public ComposedValidator(string propertyName, IEnumerable<IValidator> validators)
        {
            _propertyName = propertyName;
            registeredValidators.AddRange(validators);
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rules"></param>
        public void SetRules(IEnumerable<IValidationRule> rules)
        {
            _descriptor = new ComposedValidatorDescriptor<T>(rules, registeredValidators, _propertyName);

        }

#pragma warning disable 1591 // Xml Comments
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var errors = registeredValidators.SelectMany(x => x.Validate(context).Errors);
            return new ValidationResult(errors);
        }

        public override IValidatorDescriptor CreateDescriptor()
        {
            if (_descriptor != null) return _descriptor;
            return base.CreateDescriptor();
        }
#pragma warning restore 1591 // Xml Comments
    }
}