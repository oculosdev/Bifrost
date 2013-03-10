using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_AccessToInstanceRule
{
    [Subject(typeof (AccessToInstanceRule<>))]
    public class when_authorizing_with_instance_that_is_authorized : given.an_access_to_instance_rule
    {
        static bool is_authorized;
        static object instance;

        Establish context = () => instance = new object();

        Because of = () => is_authorized = access_to_instance_rule.IsAuthorized(instance);

        It should_delegate_to_the_service = () => instance_authorization_service.Verify(s => s.IsAuthorized(instance));
        It should_return_the_result_from_the_service = () => is_authorized.ShouldBeTrue();
    }

    [Subject(typeof(AccessToInstanceRule<>))]
    public class when_authorizing_with_instance_that_is_not_authorized : given.an_access_to_instance_rule
    {
        static bool is_authorized;

        Because of = () => is_authorized = access_to_instance_rule.IsAuthorized(unauthorized_instance);

        It should_delegate_to_the_service = () => instance_authorization_service.Verify(s => s.IsAuthorized(unauthorized_instance));
        It should_return_the_result_from_the_service = () => is_authorized.ShouldBeFalse();
    }
}