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
using Bifrost.Statistics;
namespace Bifrost.Commands
{
    /// <summary>
    /// Represents a <see cref="ICanRecordStatisticsForCommand">statistics recorder</see> that is able to record according 
    /// to the namespace the <see cref="ICommand"/> sits in
    /// </summary>
    public class CommandInGeneralStatisticsRecorder : ICanRecordStatisticsForCommand
    {
#pragma warning disable 1591 // Xml Comments
        public void Record(ICommand command, CommandResult commandResult, RecordStatisticsForCategory recordStatisticsForCategory)
        {
            recordStatisticsForCategory("General");
        }
#pragma warning restore 1591 // Xml Comments
    }
}
