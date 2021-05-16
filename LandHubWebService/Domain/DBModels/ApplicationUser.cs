using AspNetCore.Identity.MongoDbCore.Models;

using System;

namespace Domains.DBModels
{
    public class ApplicationUser : MongoIdentityUser<Guid>
    {


        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
        public ApplicationUser(string userName, string email, string displayName) : base(userName, email)
        {
            this.DisplayName = displayName;
        }
        public string DisplayName { get; set; }

    }
}
