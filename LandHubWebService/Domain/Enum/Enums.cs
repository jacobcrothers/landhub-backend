using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Enum
{
    public class Enums
    {
        public static readonly List<KeyValuePair<string, string>> PcmMailClass = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("USPS Standard Class", "Standard"),
            new KeyValuePair<string, string>("USPS First Class", "FirstClass")
        };
        public Enums()
        {
        }
        public enum WebsiteStatus
        {
            Draft = 0,
            Published = 1,
            InActive = 2,
        }
        public enum PcmDesigns
        {
            LetterTemplate = 2552
        }
    }
}
