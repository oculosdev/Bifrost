using Bifrost.Security;
using Machine.Specifications;

namespace Bifrost.Specs.Security.for_DefaultInstanceAuthorizer
{
    [Subject(typeof(DefaultInstanceAuthorizer<>))]
    public class when_checking_can_authorize : given.a_default_instance_authorizer
    {
        static object instance;
        static bool can_authorize;

        Establish context = () => instance = new object();

        Because of = () => can_authorize = default_instance_authorizer.CanAuthorize(instance);

        It should_be_able_to_authorize = () => can_authorize.ShouldBeTrue();
    }
}