using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Money.Models
{
    public class MoneyList
    {
        public string Category { get; set; }
        public double Money { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}