﻿#region License
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
using Bifrost.Commands;

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines the functionality for recording statistics for <see cref="ICommandStatistics"/>
    /// </summary>
    public interface ICommandStatistics
    {
        /// <summary>
        /// Record statistics against a <see cref="ICommand">command</see> and its <see cref="CommandResult">result</see>
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> we want to record statistics for</param>
        /// <param name="commandResult">The <see cref="CommandResult"/> we want to record statistics for</param>
        void Record(ICommand command, CommandResult commandResult);
    }
}