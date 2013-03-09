using System;
using Bifrost.Commands;
using Bifrost.Security;

namespace Bifrost.Domain
{

    /// <summary>
    /// Extensions for building a security descriptor specific for <see cref="IAggregateRoot">aggregate roots</see>
    /// </summary>
    public static class AggregateRootSecurityExtensions
    {
        /// <summary>
        /// Add a <see cref="AccessingAggregateRoot"/> security action to describe accessing of <see cref="IAggregateRoot">aggregate roots</see>
        /// </summary>
        /// <param name="descriptorBuilder"><see cref="ISecurityDescriptorBuilder"/> to extend</param>
        /// <returns><see cref="AccessingAggregateRoot"/>security action for describing the action</returns>
        public static AccessingAggregateRoot Accessing(this ISecurityDescriptorBuilder descriptorBuilder)
        {
            var action = new AccessingAggregateRoot();
            descriptorBuilder.Descriptor.AddAction(action);
            return action;
        }

        /// <summary>
        /// Add a <see cref="AggregateRootSecurityTarget"/> to the <see cref="AccessingAggregateRoot"/> security action
        /// </summary>
        /// <returns><see cref="AggregateRootSecurityTarget"/></returns>
        public static AggregateRootSecurityTarget AggregateRoots(this AccessingAggregateRoot action)
        {
            var target = new AggregateRootSecurityTarget();
            action.AddTarget(target);
            return target;
        }

        /// <summary>
        /// Add a <see cref="NamespaceSecurable"/> to the <see cref="AggregateRootSecurityTarget"/> for a given namespace
        /// </summary>
        /// <param name="target"><see cref="AggregateRootSecurityTarget"/> to add to</param>
        /// <param name="namespace">Namespace to secure</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="NamespaceSecurable"/> for the specific namespace</returns>
        public static AggregateRootSecurityTarget InNamespace(this AggregateRootSecurityTarget target, string @namespace, Action<NamespaceSecurable> continueWith)
        {
            var securable = new NamespaceSecurable(@namespace);
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }

        /// <summary>
        /// Add a <see cref="TypeSecurable"/> to the <see cref="AggregateRootSecurityTarget"/> for a given type
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IAggregateRoot"/> to secure</typeparam>
        /// <param name="target"><see cref="AggregateRootSecurityTarget"/> to add to</param>
        /// <param name="continueWith">Callback for continuing the fluent interface</param>
        /// <returns><see cref="TypeSecurable"/> for the specific type</returns>
        public static AggregateRootSecurityTarget InstanceOf<T>(this AggregateRootSecurityTarget target, Action<TypeSecurable> continueWith) where T : IAggregateRoot
        {
            var securable = new TypeSecurable(typeof(T));
            target.AddSecurable(securable);
            continueWith(securable);
            return target;
        }
    }
}
