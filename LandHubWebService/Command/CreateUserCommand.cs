using MediatR;

using System;

namespace Command
{
    public class CreateUserCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string CountryName { get; set; }
        public string[] Roles { get; set; }
        public string OrganizationId { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string DisplayName { get; set; }
        public string OrganizationTitle { get; set; }
    }
}
