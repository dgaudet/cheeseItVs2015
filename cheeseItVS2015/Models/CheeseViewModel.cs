using System;
using System.Collections.Generic;

namespace cheeseItVS2015.Models
{
    public class CheeseViewModel
    {
        public ICollection<Cheese> Cheeses { get; set; }
        public Dictionary<String, List<Decimal?>> FutureCheesePrices { get; set; }
        public String Message { get; set; }
    }
}