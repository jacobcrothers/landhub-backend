using Commands;

using MediatR;

using Services.IManagers;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class VerifyCodeAndEmailHandler : IRequestHandler<VerifyCodeAndEmail, bool>
    {
        private IBaseUserManager _userManager;
        public VerifyCodeAndEmailHandler(IBaseUserManager _baseUserManager)
        {
            this._userManager = _baseUserManager;
        }

        public async Task<bool> Handle(VerifyCodeAndEmail request, CancellationToken cancellationToken)
        {
            return await _userManager.VerifyEmail(request.Code, request.Email);
        }
    }
}
