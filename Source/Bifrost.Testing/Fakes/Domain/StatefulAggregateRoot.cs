using System;
using Bifrost.Domain;
using Bifrost.Testing.Fakes.Events;

namespace Bifrost.Testing.Fakes.Domain
{
    public class StatefulAggregateRoot : AggregateRoot
    {
        public string Value { get; set; }
        public bool EventApplied { get; private set; }

        public StatefulAggregateRoot(Guid id) : base(id)
        {
        }

        void On(SimpleEvent simpleEvent)
        {
            EventApplied = true;
            Value = simpleEvent.Id.ToString();
        }


        public bool CommitCalled = false;
        public override void Commit()
        {
            CommitCalled = true;
            base.Commit();
        }

        public bool RollbackCalled = false;
        public override void Rollback()
        {
            RollbackCalled = true;
            base.Rollback();
        }
    }
}