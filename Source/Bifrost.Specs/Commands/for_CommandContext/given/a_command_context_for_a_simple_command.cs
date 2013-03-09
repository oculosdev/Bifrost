using System;
using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Security;
using Bifrost.Testing.Fakes.Commands;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Commands.for_CommandContext.given
{
    public class a_command_context_for_a_simple_command
    {
        protected static SimpleCommand command;
        protected static CommandContext command_context;
        protected static Mock<IEventStore>  event_store_mock;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator;
        protected static Mock<IAggregateRootSecurityManager> aggregate_root_security_manager;
        protected static IAggregateRootTracker aggregate_root_tracker;

        Establish context = () =>
        {
            command = new SimpleCommand(Guid.NewGuid());
            event_store_mock = new Mock<IEventStore>();
            aggregate_root_security_manager = new Mock<IAggregateRootSecurityManager>();
            aggregate_root_security_manager.Setup(sm => sm.Authorize(Moq.It.IsAny<IAggregateRoot>())).Returns(new AuthorizationResult());
            aggregate_root_tracker = new AggregateRootTracker(aggregate_root_security_manager.Object);
                
            uncommitted_event_stream_coordinator = new Mock<IUncommittedEventStreamCoordinator>();
            command_context = new CommandContext(command, null, event_store_mock.Object, uncommitted_event_stream_coordinator.Object, aggregate_root_tracker);
        };
    }
}
