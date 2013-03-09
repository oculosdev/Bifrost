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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Bifrost.Concepts;

namespace Bifrost.Extensions
{
    /// <summary>
    /// Provides a set of extension methods to <see cref="string"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Convert a string into a camel cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToLowerInvariant();

                var firstLetter = str[0].ToString().ToLowerInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Convert a string into a pascal cased string
        /// </summary>
        /// <param name="str">string to convert</param>
        /// <returns>Converted string</returns>
        public static string ToPascalCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                if (str.Length == 1)
                    return str.ToUpperInvariant();

                var firstLetter = str[0].ToString().ToUpperInvariant();
                return firstLetter + str.Substring(1);
            }
            return str;
        }

        /// <summary>
        /// Convert a string into the desired type
        /// </summary>
        /// <param name="str">the string to parse</param>
        /// <param name="type">the desired type</param>
        /// <returns>value as the desired type</returns>
        public static object ParseTo(this string str, Type type)
        {
            if (type == typeof(Guid))
                return Guid.Parse(str);

            if (type.IsConcept())
            {
                var primitiveType = type.GetConceptValueType();
                var primitive = ParseTo(str, primitiveType);
                return ConceptFactory.CreateConceptInstance(type, primitive);
            }

            return Convert.ChangeType(str, type, null);           
        }

        /// <summary>
        /// Concatenates all the strings into a single string, separated with the supplied separator
        /// </summary>
        /// <param name="strings">strings to concat</param>
        /// <param name="separator">separator to include between each concatenated string value</param>
        /// <returns></returns>
        public static string ConcatAll(this IEnumerable<string> strings, string separator)
        {
            return strings.Aggregate(new StringBuilder(), (current, next) => current.Append(separator).Append(next)).ToString();
        }

        /// <summary>
        /// Concatenates all the strings into a single string, separated with the default separator
        /// </summary>
        /// <param name="strings">strings to concat</param>
        /// <returns></returns>
        public static string ConcatAll(this IEnumerable<string> strings)
        {
            return ConcatAll(strings, ", ");
        }
    }
}
