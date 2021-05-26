using Commands;

using Domains.DBModels;

using Infruscture;

using MediatR;

using PropertyHatchCoreService.IManagers;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class SendInvitationCommandHandler : AsyncRequestHandler<SendInvitationCommand>
    {
        private IBaseRepository<Invitation> _baseRepositoryInvitation;
        private IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate;
        private IMailManager _mailManager;
        public SendInvitationCommandHandler(IBaseRepository<Invitation> _baseRepositoryInvitation
            , IMailManager mailManager
            , IBaseRepository<EmailTemplate> _baseRepositoryEmailTemplate)
        {
            this._baseRepositoryInvitation = _baseRepositoryInvitation;
            this._mailManager = mailManager;
            this._baseRepositoryEmailTemplate = _baseRepositoryEmailTemplate;
        }

        protected override async Task Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("{@orgName}", request.OrgName);
            keyValuePairs.Add("{@senderName}", request.UserDisplayName);

            var template = await _baseRepositoryEmailTemplate.GetSingleAsync(x => x.TemplateName == Const.EMAIL_TEMPLATE_INVITATION_SEND);
            string emailTemplate = _mailManager.EmailTemplate(template.TemplateBody, keyValuePairs);

            var invitation = new Invitation
            {
                Id = Guid.NewGuid().ToString(),
                InvitedUserEmail = request.InvitationEmail,
                OrgId = request.OrgId,
                SenderId = request.UserId
            };

            await _baseRepositoryInvitation.Create(invitation);
            await _mailManager.SendEmail(new string[] { request.InvitationEmail }, null, null, template.Subject, emailTemplate);
        }
    }
}
