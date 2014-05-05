using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCard.Models
{
    class CreditCardItem
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public int RiskLevel { get; set; }
    }
}
