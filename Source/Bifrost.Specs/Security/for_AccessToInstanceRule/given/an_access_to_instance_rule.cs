using System;
using Bifrost.Security;
using Machine.Specifications;
using Moq;

namespace Bifrost.Specs.Security.for_AccessToInstanceRule.given
{
    public class an_access_to_instance_rule
    {
        protected static AccessToInstanceRule<object> access_to_instance_rule;
        protected static Mock<IInstanceAuthorizationService> instance_authorization_service;
        protected static object unauthorized_instance;

        Establish context = () =>
            {
                instance_authorization_service = new Mock<IInstanceAuthorizationService>();
                instance_authorization_service.Setup(a => a.IsAuthorized(Moq.It.IsAny<object>())).Returns((object o) => o != unauthorized_instance);

                access_to_instance_rule = new AccessToInstanceRule<object>(instance_authorization_service.Object);
            };
    }
}