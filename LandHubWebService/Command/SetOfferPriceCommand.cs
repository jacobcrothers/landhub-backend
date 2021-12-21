using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class SetOfferPriceCommand : IRequest
    {
        public string OfferPrice { get; set; }
        public List<string> PropertyIds { get; set; }
    }
}
