using System.Collections.Generic;
using Bifrost.Security;

namespace Bifrost.Testing.Fakes.Security
{
    public class FakeAuthorizationResult : AuthorizationResult
    {
        readonly bool _isAuthorized;
        readonly IEnumerable<string> _errorMessages;

        public FakeAuthorizationResult(bool isAuthorized, IEnumerable<string> errorMessages)
        {
            _isAuthorized = isAuthorized;
            _errorMessages = errorMessages;
        }

        public override bool IsAuthorized
        {
            get
            {
                return _isAuthorized;
            }
        }

        public override IEnumerable<string> BuildFailedAuthorizationMessages()
        {
            return _errorMessages;
        }
    }
}