using MediatR;

using System;

namespace Commands
{
    public class UserUpdateCommand : IRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string CountryName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string DisplayName { get; set; }
    }
}
