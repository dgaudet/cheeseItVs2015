using System;
using System.Collections.Generic;
using cheeseItVS2015.Models;
using cheeseItVS2015.Repositories;

namespace cheeseItVS2015.Services
{
    public class CheeseService : ICheeseService
    {
        private ICheeseRepository _cheeseRepo;

        public CheeseService(ICheeseRepository cheeseRepository)
        {
            _cheeseRepo = cheeseRepository;
        }

        public IEnumerable<Cheese> GetAllCheeses()
        {
            return _cheeseRepo.GetAll();
        }

        public Dictionary<String, List<Decimal?>> FutureCheesePrices(int numberOfFutureDays)
        {
            var futureCheesePrices = new Dictionary<String, List<Decimal?>>();
            var cheeses = GetAllCheeses();

            foreach (var cheese in cheeses)
            {
                var futurePrices = new List<Decimal?>();
                for (int i = 1; i <= numberOfFutureDays; i++)
                {
                    futurePrices.Add(cheese.PriceForDay(cheese.DateRecieved.AddDays(i)));
                }
                futureCheesePrices.Add(cheese.Name, futurePrices);
            }
            return futureCheesePrices;
        }
    }
}
