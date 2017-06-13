using System.Collections.Generic;
using cheeseItVS2015.Models;

namespace cheeseItVS2015.Repositories
{
    public class CheeseRepository : ICheeseRepository
    {
        private List<Cheese> _cheeseData;

        public CheeseRepository()
        {
            _cheeseData = new List<Cheese>();
        }

        public void InsertCheeses(IEnumerable<Cheese> newCheeses)
        {
            _cheeseData.Clear();
            foreach (var newCheese in newCheeses)
            {
                var duplicateCheeseName = false;
                foreach (var existingCheese in _cheeseData)
                {
                    if (existingCheese.Name == newCheese.Name)
                    {
                        duplicateCheeseName = true;
                    }
                }
                if (!duplicateCheeseName)
                {
                    _cheeseData.Add(newCheese);
                }
            }
        }

        public IEnumerable<Cheese> GetAll()
        {
            return _cheeseData;
        }
    }
}