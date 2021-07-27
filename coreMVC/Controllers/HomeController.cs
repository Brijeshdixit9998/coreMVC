using coreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace coreMVC.Controllers
{
    //#region creating deletgates manually
    //public delegate double Delegate1(int x, float y, double z);
    //public delegate void Delegate2(int x, float y, double z, out double result);
    //public delegate bool Delegate3(string obj); 
    //#endregion
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //#region Creating reference for methods defined below
            //Delegate1 obj1 = Addnums1;
            //double result = obj1.Invoke(300, 390.8f, 920.20);
            //Delegate2 obj2 = Addnums2;
            //double result2 = 0;
            //obj2.Invoke(300, 390.8f, 920.20, out result2);
            //Delegate3 obj3 = checklength;
            //bool result3 = obj3.Invoke("Brij");
            //#endregion
            //#region using inbuilt delegated instead of creating new ones to point the methods
            //Func<int, float, double, double> obj1 = Addnums1;
            //double result = obj1.Invoke(300, 390.8f, 920.20);
            //Action<int, float, double> obj2 = Addnums2;
            //obj2.Invoke(300, 390.8f, 920.20);
            //Predicate<string> obj3 = checklength;
            //Func<string,bool> obj4 = checklength; //i can use func instead of predicate because it returns a value
            //bool result3 = obj3.Invoke("Brij"); 
            //#endregion 
            #region definig the logic at the time of delegated creatiion instead of calling the function
            Func<int, float, double, double> obj1 = (x, y, z) => { return x + y + z; }; //function in a single line */
            Action<int, float, double> obj2 = (x, y, z) => { ViewBag.result2 = x + y + z; }; //not returnable 
            Predicate<string> obj3 = (str) => { return str.Length > 5 ? true : false; };
            Func<string, bool> obj4 = (str) => { return str.Length > 5 ? true : false; }; 
            obj2.Invoke(300, 390.8f, 920.20);
            double result = obj1.Invoke(300, 390.8f, 920.20);
            bool result3 = obj3.Invoke("Brij"); 
            bool result4 = obj4.Invoke("Brijesh"); 
            #endregion
            ViewBag.result1 = result;
            //ViewBag.result2 = result2;
            ViewBag.result3 = result3;
            ViewBag.result4 = result4;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Generally We Use Function
        public static double Addnums1(int x, float y, double z)
        {
            return Math.Round((x + y + z));
        }

        public static void Addnums2(int x, float y, double z)
        {
            Math.Round((x + y + z));
        }

        //public static void Addnums2(int x, float y, double z,out double result)
        //{
        //    result = Math.Round((x + y + z));
        //}

        public static bool checklength(string obj)
        {
            return obj.Length > 5 ? true : false;
        } 
        #endregion
    }
}
