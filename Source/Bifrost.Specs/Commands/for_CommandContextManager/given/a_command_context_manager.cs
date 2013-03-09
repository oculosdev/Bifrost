using Bifrost.Commands;
using Bifrost.Domain;
using Bifrost.Events;
using Bifrost.Sagas;
using Bifrost.Security;
using Machine.Specifications;
using Moq;
using Bifrost.Execution;

namespace Bifrost.Specs.Commands.for_CommandContextManager.given
{
	public class a_command_context_manager
	{
		protected static CommandContextManager Manager;
        protected static Mock<IUncommittedEventStreamCoordinator> uncommitted_event_stream_coordinator;
        protected static Mock<IProcessMethodInvoker> process_method_invoker_mock;
        protected static Mock<ISagaLibrarian> saga_librarian_mock;
        protected static Mock<IExecutionContextManager> execution_context_manager_mock;
        protected static Mock<IEventStore> event_store_mock;
	    protected static Mock<IAggregateRootSecurityManager> aggregate_root_security_manager;
	    protected static IAggregateRootTracker aggregate_root_tracker;

		Establish context = () =>
		                            	{
											CommandContextManager.ResetContext();
                                            uncommitted_event_stream_coordinator = new Mock<IUncommittedEventStreamCoordinator>();
		                            		event_store_mock = new Mock<IEventStore>();
		                            		process_method_invoker_mock = new Mock<IProcessMethodInvoker>();
		                            		saga_librarian_mock = new Mock<ISagaLibrarian>();
                                            execution_context_manager_mock = new Mock<IExecutionContextManager>();
		                            	    aggregate_root_security_manager = new Mock<IAggregateRootSecurityManager>();
                                            aggregate_root_security_manager.Setup(sm => sm.Authorize(Moq.It.IsAny<IAggregateRoot>())).Returns(new AuthorizationResult());
                                            aggregate_root_tracker = new AggregateRootTracker(aggregate_root_security_manager.Object);
                                           
		                            		Manager = new CommandContextManager(
												uncommitted_event_stream_coordinator.Object, 
												saga_librarian_mock.Object,
												process_method_invoker_mock.Object,
                                                execution_context_manager_mock.Object,
                                                event_store_mock.Object,
                                                aggregate_root_tracker);
		                            	};
	}
}
