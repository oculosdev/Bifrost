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
using System.Diagnostics;
using Bifrost.Aspects;
using Bifrost.Insight;

namespace Bifrost.Commands.Insight
{
    /// <summary>
    /// Represents specific aspects for <see cref="ITelemetry"/>
    /// </summary>
    public class TelemetryAspectsForCommandCoordinator : IAspectsFor<ICommandCoordinator>
    {
        ITelemetry _telemetry;
        Stopwatch _stopwatch;

        /// <summary>
        /// Initializes a new instance of <see cref="TelemetryAspectsForCommandCoordinator"/>
        /// </summary>
        /// <param name="telemetry"></param>
        public TelemetryAspectsForCommandCoordinator(ITelemetry telemetry)
        {
            _telemetry = telemetry;
            _stopwatch = new Stopwatch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="command"></param>
        public void AroundHandle(IInvocation invocation, ICommand command)
        {
            _stopwatch.Start();
            invocation.Proceed();
            _stopwatch.Stop();

            var elapsed = _stopwatch.Elapsed;
        }
    }
}
