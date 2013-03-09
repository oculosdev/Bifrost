using Bifrost.Domain;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Domain.for_AggregateRootTracker.given
{
    public class an_aggregate_root_tracker
    {
        protected static IAggregateRootTracker tracker;
        protected static Mock<IAggregateRootSecurityManager> security_manager;
        protected static IAggregateRoot aggregate_root_that_is_not_authorized_to_access;

        Establish context = () =>
            {
                aggregate_root_that_is_not_authorized_to_access = new Mock<IAggregateRoot>().Object;
                security_manager = new Mock<IAggregateRootSecurityManager>();
                security_manager.Setup(sm => sm.Authorize(Moq.It.IsAny<IAggregateRoot>()))
                                .Returns((IAggregateRoot aggRoot) => IsAuthorized(aggRoot));

                tracker = new AggregateRootTracker(security_manager.Object);
            };

        static AuthorizationResult IsAuthorized(IAggregateRoot aggRoot)
        {
            return aggRoot == aggregate_root_that_is_not_authorized_to_access
                       ? new FakeAuthorizationResult(false, new[] {"Hands off!"})
                       : new AuthorizationResult();
        }
    }
}