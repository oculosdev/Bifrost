using Bifrost.Validation;
using FluentValidation;

namespace Bifrost.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentValidationRuleExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="validator"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TV> DynamicValidationRule<T, TV>(this IRuleBuilder<T, TV> ruleBuilder, IValidator validator, string name) where TV: IAmValidatable
        {
#pragma warning disable 0618
            return ruleBuilder
                .NotNull()
                .SetValidator(validator)
                .WithName(name);
#pragma warning restore 0618
        }
    }
}