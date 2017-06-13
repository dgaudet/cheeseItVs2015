using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }

        // POST: LoadFile/Load
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Load(HttpPostedFileBase file)
        {
            if (file?.FileName != null)
            {
                var recievedDate = DateFromFileName(file.FileName);
                if (recievedDate == DateTime.MinValue)
                {
                    ViewBag.result = "Please Enter a file with a valid file format.";
                    ViewBag.resultCssClass = "alert alert-danger";
                    ViewBag.showLink = false;
                }
                else
                {
                    try
                    {
                        if (file.ContentLength > 0)
                        {
                            var numCheesesLoaded = _cheeseService.LoadCheeses(file, recievedDate);

                            ViewBag.result = $"Successfully loaded {numCheesesLoaded} Cheeses.";
                            ViewBag.resultCssClass = "alert alert-success";
                            ViewBag.showLink = true;
                        }
                        else
                        {
                            ViewBag.result = "No Cheeses loaded.";
                            ViewBag.resultCssClass = "alert alert-warning";
                            ViewBag.showLink = false;
                        }
                    }
                    catch (Exception)
                    {
                        ViewBag.result = "There was a problem with the your Cheese file. Please try a different file.";
                        ViewBag.resultCssClass = "alert alert-danger";
                        ViewBag.showLink = false;
                    }
                }
            }

            return View();
        }

        private DateTime DateFromFileName(string fileName)
        {
            DateTime fileDate;

            var dateString = "";
            if (fileName.IndexOf("_") > 0 && fileName.IndexOf(".xml") > 0)
            {
                try
                {
                    var dateWithExtension = fileName.Substring(fileName.IndexOf("_") + 1, fileName.Length - fileName.IndexOf("_") - 1);
                    dateString = dateWithExtension.Substring(0, dateWithExtension.IndexOf(".xml"));
                }
                catch (Exception)
                {
                }
            }
            
            DateTime.TryParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fileDate);

            return fileDate;
        }
    }
}