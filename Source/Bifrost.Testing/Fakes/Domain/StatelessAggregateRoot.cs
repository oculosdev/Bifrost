using System;
using Bifrost.Domain;

namespace Bifrost.Testing.Fakes.Domain
{
    public class StatelessAggregateRoot : AggregateRoot
    {
        public StatelessAggregateRoot(Guid id) : base(id) {}
    }
}