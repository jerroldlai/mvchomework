using Money.Views.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Money.Controllers
{
    public class MoneyController : Controller
    {
        // GET: Money
        public ActionResult Money()
        {
            return View();
        }
        
        public ActionResult List(MoneyViewModel pagedata)
        {
            var source = new List<MoneyViewModel>();
            if (pagedata.category!= null || 
                pagedata.money != null ||
                pagedata.description != null ||
                pagedata.date != null)
            {
                source.Add(new MoneyViewModel
                {
                    category =pagedata.category,
                    money =pagedata.money,
                    date =pagedata.date,
                    description =pagedata.description,
                });
            }

            for (int i = 0; i < 5; i++)
            {
                source.Add(new MoneyViewModel
                {
                    category = "支出",
                    money = System.Convert.ToString(123 * i),
                    date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"),
                    description = "",
                });
            }
            return View(source);
        }
    }
}