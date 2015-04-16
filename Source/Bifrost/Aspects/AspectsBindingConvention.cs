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
using Bifrost.Commands;
using Bifrost.Execution;

namespace Bifrost.Aspects
{
    /// <summary>
    /// Represents a <see cref="IBindingConvention"/> for all aspects in the system
    /// </summary>
    public class AspectsBindingConvention : IBindingConvention
    {
        IContainer _container;

        Dictionary<Type, IEnumerable<Type>> _aspects;

        /// <summary>
        /// Initializes a new instance of <see cref="AspectsBindingConvention"/>
        /// </summary>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> for discovering aspects</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances</param>
        public AspectsBindingConvention(ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _container = container;

            var aspectTypes = typeDiscoverer.FindMultiple(typeof(IAspectsFor<>));
            _aspects = aspectTypes
                .GroupBy(
                    t => t.GetInterface(typeof(IAspectsFor<>).Name).GenericTypeArguments[0]
                ).ToDictionary(g=>g.Key, g=>g.AsEnumerable());
        }


#pragma warning disable 1591 // Xml Comments

        public bool CanResolve(IContainer container, Type service)
        {
            var hasAspects = _aspects.ContainsKey(service);
            return hasAspects;
        }

        public void Resolve(IContainer container, Type service)
        {
            container.Bind(service, () =>
            {
                var aspectsProxyGenerator = container.Get<IAspectsProxyGenerator>();
                var aspectInstances = _aspects[service].Select(t => container.Get(t) as IHaveAspects);
                var proxy = aspectsProxyGenerator.GetFor(aspectInstances);
                return proxy;
            });
        }
#pragma warning restore 1591 // Xml Comments
    }
}
