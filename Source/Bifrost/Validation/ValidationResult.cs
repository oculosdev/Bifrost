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
using System.Collections.Generic;

namespace Bifrost.Validation
{
    /// <summary>
    /// Represents a container for the results of a validation request
    /// </summary>
    public class ValidationResult
    {

        /// <summary>
        /// Initializes an instance of <see cref="ValidationResult"/>
        /// </summary>
        /// <param name="errorMessage">Message related to the validation result</param>
        public ValidationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Initializes an instance of <see cref="ValidationResult"/>
        /// </summary>
        /// <param name="errorMessage">Message related to the validation result</param>
        /// <param name="memberNames">Member names that are involved</param>
        public ValidationResult(string errorMessage, IEnumerable<string> memberNames)
        {
            ErrorMessage = errorMessage;
            MemberNames = memberNames;
        }

        /// <summary>
        /// Gets the error message for the validation
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the collection of member names that indicate which fields or properties
        /// have validation errors
        /// </summary>
        public IEnumerable<string> MemberNames { get; private set; }
    }
}
