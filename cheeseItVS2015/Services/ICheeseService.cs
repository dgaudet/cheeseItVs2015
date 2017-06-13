using System;
using System.Collections.Generic;
using cheeseItVS2015.Models;

namespace cheeseItVS2015.Services
{
    public interface ICheeseService
    {
        IEnumerable<Cheese> GetAllCheeses();
        Dictionary<String, List<Decimal?>> FutureCheesePrices(int numberOfFutureDays);
    }
}