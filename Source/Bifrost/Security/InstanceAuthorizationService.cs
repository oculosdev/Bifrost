using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.Security
{
    /// <summary>
    /// Represents an instance of <see cref="IInstanceAuthorizationService"/>
    /// </summary>
    [Singleton]
    public class InstanceAuthorizationService : IInstanceAuthorizationService
    {
        readonly IInstanceAuthorizerProvider _instanceAuthorizerProvider;

        /// <summary>
        /// Creates an instance of <see cref="InstanceAuthorizationService"/>
        /// </summary>
        /// <param name="instanceAuthorizerProvider"></param>
        public InstanceAuthorizationService(IInstanceAuthorizerProvider instanceAuthorizerProvider)
        {
            _instanceAuthorizerProvider = instanceAuthorizerProvider;
        }

#pragma warning disable 1591 // Xml Comments
        public bool IsAuthorized<T>(T instance) where T : class
        {
            return _instanceAuthorizerProvider.GetInstanceAuthorizerFor<T>().IsAuthorized(instance);
        }
#pragma warning restore 1591 // Xml Comments  


    }

    /// <summary>
    /// Defines a provider that returns object-specific authorizers
    /// </summary>
    public interface IInstanceAuthorizerProvider
    {
        /// <summary>
        /// Gets an instance of an <see cref="IInstanceAuthorizer{T}"/> that can authorize an instance of the type
        /// </summary>
        /// <typeparam name="T">Type of the instance to be authorized</typeparam>
        /// <returns>An instance of <see cref="IInstanceAuthorizer{T}"/></returns>
        IInstanceAuthorizer<T> GetInstanceAuthorizerFor<T>() where T : class;
    }

    /// <summary>
    /// Represents an instance of an <see cref="IInstanceAuthorizerProvider" />
    /// </summary>
    public class InstanceAuthorizerProvider : IInstanceAuthorizerProvider
    {
        ITypeDiscoverer _typeDiscoverer;
        IContainer _container;
        readonly Dictionary<Type, object> _instanceAuthorizersRegistry;

        /// <summary>
        /// Instantiates an instance of <see cref="InstanceAuthorizerProvider"/>
        /// </summary>
        /// <param name="container"><see cref="IContainer"/>Container for creating <see cref="IInstanceAuthorizer{T}"/> instances</param>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer" /> for finding implementations of <see cref="IInstanceAuthorizer{T}"/></param>
        public InstanceAuthorizerProvider(IContainer container, ITypeDiscoverer typeDiscoverer)
        {
            _container = container;
            _typeDiscoverer = typeDiscoverer;
            _instanceAuthorizersRegistry = new Dictionary<Type, object>();

            PopulateAuthorizers();
        }

#pragma warning disable 1591 // Xml Comments
        public IInstanceAuthorizer<T> GetInstanceAuthorizerFor<T>() where T : class
        {
            object authorizer;
            if (_instanceAuthorizersRegistry.TryGetValue(typeof(T), out authorizer))
                return authorizer as IInstanceAuthorizer<T>;

            return new DefaultInstanceAuthorizer<T>();
        }
#pragma warning restore 1591 // Xml Comments  

        void PopulateAuthorizers()
        {
            var authorizers = _typeDiscoverer.FindMultiple(typeof(IInstanceAuthorizer<>));
            authorizers.ForEach(Register);
        }

        void Register(Type typeToRegister)
        {
            var typeOfInstance = GetInstanceType(typeToRegister);

            if (typeOfInstance == null || typeOfInstance.IsInterface || typeOfInstance.IsAbstract || _instanceAuthorizersRegistry.ContainsKey(typeOfInstance) || typeOfInstance == typeof(DefaultInstanceAuthorizer<>))
                return;

            _instanceAuthorizersRegistry.Add(typeOfInstance, _container.Get(typeToRegister));
        }

        Type GetInstanceType(Type typeToRegister)
        {
            var types = from interfaceType in typeToRegister.GetInterfaces()
                        where interfaceType.IsGenericType
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == typeof(IInstanceAuthorizer<>)
                        select interfaceType.GetGenericArguments().FirstOrDefault();

            return types.FirstOrDefault();
        }
    }
}