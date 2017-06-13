using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cheeseItVS2015.Models;
using cheeseItVS2015.Repositories;
using cheeseItVS2015.Services;

namespace cheeseItVS2015.Controllers
{
    public class CheeseController : Controller
    {
        private readonly ICheeseService _cheeseService;

        public CheeseController()
        {
            var cheeseRepo = CheeseRepositoryFactory.SharedInstance();
            _cheeseService = new CheeseService(cheeseRepo);
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            var cheeses = _cheeseService.GetAllCheeses().ToArray();
            var futureCheesePrices = _cheeseService.FutureCheesePrices(7);

            var message = "";
            if (cheeses.Length == 0)
            {
                message = "There are no cheeses loaded yet, please load some cheeses";
            }

            var viewModel = new CheeseViewModel
            {
                Cheeses = cheeses,
                FutureCheesePrices = futureCheesePrices,
                Message = message
            };
            return View(viewModel);
        }
    }
}