using System;
using System.Web;

namespace cheeseItVS2015.Services
{
    public interface ICheeseLoaderService
    {
        int LoadCheeses(string fileName, DateTime dateRecieved);
        int LoadCheeses(HttpPostedFileBase file, DateTime dateRecieved);
    }
}