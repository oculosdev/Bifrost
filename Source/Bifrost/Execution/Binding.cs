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
namespace Bifrost.Execution
{
    /// <summary>
    /// Represents a binding for the <see cref="IContainer"/>
    /// </summary>
    public class Binding
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Binding"/>
        /// </summary>
        public Binding(Type source)
        {
            Lifecycle = BindingLifecycle.Transient;
            Source = source;
        }


        /// <summary>
        /// Gets the source type
        /// </summary>
        public Type Source { get; private set; }

        /// <summary>
        /// Gets or sets the target type
        /// </summary>
        public Type Target { get; set; }

        /// <summary>
        /// Gets or sets the target instance
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// Gets or sets the target instance callback for retrieving an instance
        /// </summary>
        public Func<object> InstanceCallback { get; set; }

        /// <summary>
        /// Gets or sets the target callback for retrieving the target type
        /// </summary>
        public Func<Type> TargetCallback { get; set; }

        

        /// <summary>
        /// Gets or sets the <see cref="BindingLifecycle"/> for the <see cref="Binding"/>
        /// </summary>
        public BindingLifecycle Lifecycle { get; set; }
    }
}
