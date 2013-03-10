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
using Bifrost.Commands;
using Bifrost.Execution;
using Bifrost.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bifrost
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandStatistics"/>
    /// </summary>
    [Singleton]
    public class CommandStatistics : ICommandStatistics
    {
        /// <summary>
        /// The Context used during statistics recording
        /// </summary>
        public const string ContextName = "Command";

        /// <summary>
        /// Represents the successful handling event
        /// </summary>
        public const string SuccessEvent = "WasHandled";

        /// <summary>
        /// Represents an invalid command event
        /// </summary>
        public const string InvalidEvent = "HadValidationErrors";

        /// <summary>
        /// Represents an event for commands with an exception
        /// </summary>
        public const string HasExceptionEvent = "HasException";

        /// <summary>
        /// Represents an event for non authorized commands
        /// </summary>
        public const string NotAuthorizedEvent = "DidNotPassSecurity";

        IStatisticsStore _statisticsStore;
        ITypeDiscoverer _typeDiscoverer;
        IEnumerable<Type> _recorderTypes;
        IContainer _container;

        /// <summary>
        /// Initializes an instance of <see cref="CommandStatistics"/>
        /// </summary>
        /// <param name="statisticsStore"><see cref="IStatisticsStore"/> to use for storing statistics</param>
        /// <param name="typeDiscoverer"><see cref="ITypeDiscoverer"/> for discovering recorders</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances of recorders</param>
        public CommandStatistics(IStatisticsStore statisticsStore, ITypeDiscoverer typeDiscoverer, IContainer container)
        {
            _statisticsStore = statisticsStore;
            _typeDiscoverer = typeDiscoverer;
            _container = container;
            _recorderTypes = _typeDiscoverer.FindMultiple<ICanRecordStatisticsForCommand>();
        }

#pragma warning disable 1591 // Xml Comments
        public void Record(ICommand command, CommandResult commandResult)
        {
            try
            {
                var eventName = GetEventNameFromCommandResult(commandResult);

                foreach (var recorderType in _recorderTypes)
                {
                    var recorder = _container.Get(recorderType) as ICanRecordStatisticsForCommand;
                    recorder.Record(command, commandResult, category => _statisticsStore.Record(ContextName, eventName, recorderType.Name, category));
                }
            }
            catch
            {
                // We can't allow the system to not work if statistics doesn't work
                // Todo: Log error to logging system
            }
        }
#pragma warning restore 1591 // Xml Comments

        string GetEventNameFromCommandResult(CommandResult commandResult)
        {
            if (commandResult.Success) return SuccessEvent;
            if (commandResult.Invalid) return InvalidEvent;
            if (commandResult.HasException) return HasExceptionEvent;
            if (!commandResult.PassedSecurity) return NotAuthorizedEvent;
            return "unknown";            
        }
    }
}
