using System.Collections.Generic;
using cheeseItVS2015.Models;

namespace cheeseItVS2015.Repositories
{
    public interface ICheeseRepository
    {
        void InsertCheeses(IEnumerable<Cheese> newCheeses);
        IEnumerable<Cheese> GetAll();
    }

    public static class CheeseRepositoryFactory
    {
        private static ICheeseRepository _cheeseRepo;
        public static ICheeseRepository SharedInstance()
        {
            if (_cheeseRepo == null)
            {
                _cheeseRepo = new CheeseRepository();
            }
            
            return _cheeseRepo;
        }
    }
}