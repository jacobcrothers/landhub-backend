namespace Domains.DBModels
{
    public class Invitation : BaseEntity
    {
        public string SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string OrgId { get; set; }
        public string InvitedUserEmail { get; set; }
    }
}
