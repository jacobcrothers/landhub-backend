
using System;
using System.Collections.Generic;

namespace Domains.DBModels
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }

        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string CountryName { get; set; }

        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public User()
        {
            this.Roles = new List<string>();
        }

    }
}
