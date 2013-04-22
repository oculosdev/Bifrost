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
using NHibernate;

namespace Bifrost.NHibernate.Read
{
    public class ReadOnlySession : IReadOnlySession
    {
        ISession _session;
        ISessionFactory _sessionFactory;

        public ReadOnlySession(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession GetCurrentSession()
        {
            return _session ?? (_session = BuildSession());
        }

        ISession BuildSession()
        {
            var session = _sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Never;

            session.BeginTransaction(); 
            return session;
        }
    }
}