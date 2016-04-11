using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Money.Models;

namespace Money.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var source = new List<MoneyList>();

            for (int i = 0; i < 10; i++)
            {
                source.Add(new MoneyList {
                    Category = "支出",
                    Date = DateTime.Now.AddDays(i),
                    Money = 123*i
                });
            }
            return View(source);
        }
    }
}