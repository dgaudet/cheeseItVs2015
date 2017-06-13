using System;
using System.Web;
using System.Web.Mvc;
using cheeseItVS2015.Models.LoadFile;
using cheeseItVS2015.Repositories;
using cheeseItVS2015.Services;

namespace cheeseItVS2015.Controllers
{
    public class LoadFileController : Controller
    {
        private readonly ICheeseLoaderService _cheeseService;

        public LoadFileController()
        {
            var cheeseRepo = CheeseRepositoryFactory.SharedInstance();
            _cheeseService = new CheeseLoaderService(cheeseRepo);
        }

        // GET: /<controller>/
        public ActionResult Load()
        {
            return View(new LoadFileViewModel());
        }

        // POST: LoadFile/Load
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Load(LoadFileViewModel model, HttpPostedFileBase file)
        {
            //var model = new LoadFileViewModel();
            if (model.RecievedDate == DateTime.MinValue)
            {
                model.DateError = true;
            }
            if (file == null)
            {
                model.FileError = true;
            }
            if (!model.DateError && !model.FileError)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        var dateRecieved = model.RecievedDate;
                        var numCheesesLoaded = _cheeseService.LoadCheeses(file, dateRecieved);

                        ViewBag.result = $"Successfully loaded {numCheesesLoaded} Cheeses.";
                        ViewBag.resultCssClass = "alert alert-success";
                        ViewBag.showLink = true;
                    }
                    else
                    {
                        ViewBag.result = $"No Cheeses loaded.";
                        ViewBag.resultCssClass = "alert alert-warning";
                        ViewBag.showLink = false;
                    }
                }
                catch (Exception)
                {
                    ViewBag.result = $"There was a problem with the your Cheese file. Please try a different file.";
                    ViewBag.resultCssClass = "alert alert-danger";
                    ViewBag.showLink = false;
                }

                return View();
            }

            return View(model);
        }
    }
}