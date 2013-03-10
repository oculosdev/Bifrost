#region License
//
// Copyright (c) 2008-2013, Dolittle (http://www.dolittle.com)
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

namespace Bifrost.Statistics
{
    /// <summary>
    /// Represents an entry for statistics
    /// </summary>
    public class StatisticEntry
    {
        /// <summary>
        /// Gets or sets the identifier of the entry
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the context for the entry
        /// </summary>
        public string Context { get; set; }

        /// <summary>
        /// Gets or sets the event for the entry
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets the owner of the category for the entry
        /// </summary>
        public string CategoryOwner { get; set; }

        /// <summary>
        /// Gets or sets the category for the entry
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the count for the entry - the value we're measuring
        /// </summary>
        public long Count { get; set; }
    }
}
