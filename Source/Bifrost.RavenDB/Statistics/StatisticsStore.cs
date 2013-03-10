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
using System.Transactions;
using Bifrost.Statistics;
using Raven.Abstractions.Exceptions;
using Raven.Client.Document;

namespace Bifrost.RavenDB.Statistics
{
    public class StatisticsStore : IStatisticsStore
    {
        static object LockObject = new object();

        const string CollectionName = "Statistics";
        StatisticsStoreConfiguration _configuration;
        DocumentStore _documentStore;

        public StatisticsStore(StatisticsStoreConfiguration configuration)
        {
            _configuration = configuration;
            InitializeDocumentStore();
        }

        void InitializeDocumentStore()
        {
            _documentStore = _configuration.CreateDocumentStore();
        }

        public void Record(string context, string @event, string categoryOwner, string category)
        {
            using (var session = _documentStore.OpenSession())
            {
                var key = string.Format("{0}, {1}, {2}, {3}", context, @event, categoryOwner, category);
                var hashKey = key.GetHashCode();

                lock (LockObject)
                {
                    using (new TransactionScope(TransactionScopeOption.Suppress))
                    {
                        while (true)
                        {
                            try
                            {
                                var statistics = session.Load<StatisticEntry>(hashKey);
                                if (statistics == null)
                                    statistics = new StatisticEntry
                                    {
                                        Id = hashKey,
                                        Context = context,
                                        Event = @event,
                                        CategoryOwner = categoryOwner,
                                        Category = category
                                    };

                                statistics.Count++;
                                session.Store(statistics);
                                session.SaveChanges();
                                break;
                            }
                            catch (ConcurrencyException)
                            {
                                // expected, retry
                            }
                        }
                    }
                }
            }
        }
    }
}
