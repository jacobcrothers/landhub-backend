using Commands;
using Domains.DBModels;
using MediatR;
using PropertyHatchCoreService.Services;
using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Domains.Dtos.Pcm;
using Domains.Enum;

namespace CommandHandlers
{
    public class CampaignMailerCommandHandler : IRequestHandler<CampaignMailerCommand, string>
    {
        private IBaseRepository<Campaign> _campaignRepository;
        private IBaseRepository<Properties> _propertyRepository;
        private IBaseRepository<Organization> _organizationRepository;
        private PostCardManiaService _postCardManiaService;
        public CampaignMailerCommandHandler(IBaseRepository<Campaign> campaignRepository,
            IBaseRepository<Properties> propertyRepository,
            IBaseRepository<Organization> organizationRepository,
            PostCardManiaService postCardManiaService)
        {
            this._campaignRepository = campaignRepository;
            this._propertyRepository = propertyRepository;
            this._organizationRepository = organizationRepository;
            this._postCardManiaService = postCardManiaService;
        }

        public async Task<string> Handle(CampaignMailerCommand request, CancellationToken cancellationToken)
        {
            var campaigns = (from campaign in this._campaignRepository.GetAllAsync(x => x.StartDate <= DateTime.Now &&
                                                                                                DateTime.Now <= x.EndDate &&
                                                                                                x.Status == "Active").Result
                                   where campaign.Properties.Any(y => y.CampaignMailCount == 0)
                                   select campaign).ToList();
            if (campaigns.Count == 0)
                return "";
        // replace placeholders and generate a pdf
        // upload to bucket
        // https://drive.google.com/file/d/1LnDcoSx0a0V6nrQJQY3m4ETnUnMRNSDz/view?usp=sharing
            var orders = new List<Order>();
            foreach(var campaign in campaigns)
            {
                var organization = await _organizationRepository.GetByIdAsync(campaign.OrganizationId);
                var order = new Order();
                order.ExtRefNbr = campaign.Id;
                order.OrderConfig = new OrderConfig()
                {
                    DesignId = (int)Enums.PcmDesigns.LetterTemplate,
                    MailClass = Enums.PcmMailClass.Find(x => x.Key == campaign.ShippingClass).Value,
                    MailTrackingEnabled = false,
                    GlobalDesignVariables = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("returnaddress", organization.Address),
                        new KeyValuePair<string, string>("returnaddresscity", ""), //TODO: Fill
                        new KeyValuePair<string, string>("returnaddressname", ""),
                        new KeyValuePair<string, string>("returnaddressstate", ""),
                        new KeyValuePair<string, string>("returnaddresszipcode", "")
                    }
                };
                order.RecipientList = new List<Recipient>();
                foreach(var property in campaign.Properties)
                {
                    if (property.CampaignMailCount > 0)
                        continue;
                    var propertyDetail = await _propertyRepository.GetByIdAsync(property.Id);
                    var recipient = new Recipient()
                    {
                        FirstName = propertyDetail.Owner1FName,
                        LastName = propertyDetail.Owner1LName,
                        Address = propertyDetail.MailAddress,
                        Address2 = "",
                        City = propertyDetail.MCity,
                        State = propertyDetail.MState,
                        ZipCode = propertyDetail.PZip,
                        ExtRefNbr = propertyDetail.Id,
                        RecipientDesignVariables = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pdfurl","https://drive.google.com/file/d/1LnDcoSx0a0V6nrQJQY3m4ETnUnMRNSDz/view?usp=sharing")
                        }
                    };
                    if(string.IsNullOrEmpty(recipient.FirstName) || string.IsNullOrEmpty(recipient.LastName)) // company owner
                    {
                        recipient.FirstName = propertyDetail.Owner1LName;
                        recipient.LastName = propertyDetail.Owner1LName;
                    }
                    order.RecipientList.Add(recipient);
                }
                orders.Add(order);
            }
            await _postCardManiaService.PlaceNewOrder(orders);
            return "";
        }
    }
}
