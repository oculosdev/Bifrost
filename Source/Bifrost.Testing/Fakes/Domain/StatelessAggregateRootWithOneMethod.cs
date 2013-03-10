using System;
using Bifrost.Domain;

namespace Bifrost.Testing.Fakes.Domain
{
    public class StatelessAggregateRootWithOneMethod : AggregateRoot
    {
        public StatelessAggregateRootWithOneMethod(Guid id) : base(id)
        {
        }

        public static void ResetState()
        {
            DoSomethingCalled = false;
        }

        public static bool DoSomethingCalled = false;
        public void DoSomething(string input)
        {
            DoSomethingCalled = true;
        }
    }
}