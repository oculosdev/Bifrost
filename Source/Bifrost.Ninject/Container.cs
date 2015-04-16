#region License
//
// Copyright (c) 2008-2015, Dolittle (http://www.dolittle.com)
//
// Licensed under the MIT License (http://opensource.org/licenses/MIT)
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://github.com/dolittle/Bifrost/blob/master/MIT-LICENSE.txt
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Execution;
using Ninject;
using Ninject.Parameters;

namespace Bifrost.Ninject
{
    public class Container : IContainer
    {
        Dictionary<Type, Binding> _bindings = new Dictionary<Type, Binding>();

        public Container(IKernel kernel)
        {
            Kernel = kernel;
        }

        public IKernel Kernel { get; private set; }

        public T Get<T>()
        {
            return (T)Get(typeof(T), false);
        }

        public T Get<T>(bool optional)
        {
            return (T)Get(typeof(T), optional);
        }

        public object Get(Type type)
        {
            return Get(type, false);
        }

        public object Get(Type type, bool optional)
        {
            var request = Kernel.CreateRequest(type, null, new IParameter[0], optional, true);
            return Kernel.Resolve(request).SingleOrDefault();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return Kernel.GetAll<T>();
        }

        public bool HasBindingFor(Type type)
        {
            return Kernel.GetBindings(type).Count() != 0;
        }

        public bool HasBindingFor<T>()
        {
            return HasBindingFor(typeof (T));
        }

        public IEnumerable<object> GetAll(Type type)
        {
            return Kernel.GetAll(type);
        }

        public IEnumerable<Type> GetBoundServices()
        {
            return _bindings.Select(k => k.Key);
        }

        public void Bind(Type type, Func<Type> resolveCallback)
        {
            Kernel.Bind(type).ToMethod(c => resolveCallback());
            _bindings.Add(type, new Binding(type) { TargetCallback = resolveCallback });
        }

        public void Bind<T>(Func<Type> resolveCallback)
        {
            throw new NotImplementedException();
        }

        public void Bind(Type type, Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Bind<T>(Func<Type> resolveCallback, BindingLifecycle lifecycle)
        {
            throw new NotImplementedException();
        }

        public void Bind<T>(Type type)
        {
            Kernel.Bind<T>().To(type);
            _bindings.Add(typeof(T), new Binding(typeof(T)) { Target = type });
        }

        public void Bind(Type service, Type type)
        {
            Kernel.Bind(service).To(type);
            _bindings.Add(service, new Binding(service) { Target = type});
        }

        public void Bind<T>(Type type, BindingLifecycle lifecycle)
        {
            Kernel.Bind<T>().To(type).WithLifecycle(lifecycle);
            _bindings.Add(typeof(T), new Binding(typeof(T)) { Target = type, Lifecycle = lifecycle });
        }

        public void Bind(Type service, Type type, BindingLifecycle lifecycle)
        {
            Kernel.Bind(service).To(type).WithLifecycle(lifecycle);
            _bindings.Add(service, new Binding(service) { Target = type, Lifecycle = lifecycle });
        }

        public void Bind<T>(T instance)
        {
            Kernel.Bind<T>().ToConstant(instance);
            _bindings.Add(typeof(T), new Binding(typeof(T)) { Instance = instance });
        }

        public void Bind(Type service, object instance)
        {
            Kernel.Bind(service).ToConstant(instance);
            _bindings.Add(service, new Binding(service) { Instance = instance });
        }


        public void Bind<T>(Func<T> resolveCallback)
        {
            Kernel.Bind<T>().ToMethod(c => resolveCallback());
            _bindings.Add(typeof(T), new Binding(typeof(T)) { InstanceCallback = () => resolveCallback });
        }

        public void Bind(Type service, Func<Type, object> resolveCallback)
        {
            Kernel.Bind(service).ToMethod(c => resolveCallback(c.Request.Service));
            _bindings.Add(service, new Binding(service) { InstanceCallback = () => resolveCallback });
        }

        public void Bind<T>(Func<T> resolveCallback, BindingLifecycle lifecycle)
        {
            Kernel.Bind<T>().ToMethod(c => resolveCallback()).WithLifecycle(lifecycle);
            _bindings.Add(typeof(T), new Binding(typeof(T)) { InstanceCallback = () => resolveCallback, Lifecycle = lifecycle });
        }

        public void Bind(Type service, Func<Type, object> resolveCallback, BindingLifecycle lifecycle)
        {
            Kernel.Bind(service).ToMethod(c => resolveCallback(c.Request.Service)).WithLifecycle(lifecycle);
            _bindings.Add(service, new Binding(service) { InstanceCallback = () => resolveCallback });
        }

        public BindingLifecycle DefaultLifecycle { get; set; }


        public void Unbind<T>()
        {
            Kernel.Unbind<T>();
            _bindings.Remove(typeof(T));
        }

        public void Unbind(Type type)
        {
            Kernel.Unbind(type);
            _bindings.Remove(type);
        }

        public Binding GetBindingFor(Type type)
        {
            return _bindings[type];
        }
    }
}
