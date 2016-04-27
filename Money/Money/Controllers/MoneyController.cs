using Money.Models;
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
        private AccountDao dao = new AccountDao();
        List<SelectListItem> listItems = new List<SelectListItem>();
        // GET: Money
        public ActionResult Money()
        {
            Initial_Items();
            return View();
        }
        [HttpPost]
        public ActionResult Money(MoneyViewModel pagedata)
        {
            Initial_Items();
            if (ModelState.IsValid)
            {
                int row = dao.Insert(pagedata);
            }
            return View();
        }
        public ActionResult List()
        {
            var AccountLists = dao.GetAllLists();
            ViewBag.DetermineColor = new Func<string, string>(DetermineColor);
            return View(AccountLists.ToList());
        }
        private string DetermineColor(string category)
        {
            if (category.Equals("支出"))
                return "<span style=\"color: red;\">" + category + "</span>";
            else if (category.Equals("收入"))
                return "<span style=\"color: blue;\">" + category + "</span>";
            else
                return category;
        }
       
        [HttpGet]
        public ActionResult Delete(MoneyViewModel pagedata)
        {
            MoneyViewModel instance = dao.GetSingleRow(pagedata.id);
            if (instance == null)
            {
                return HttpNotFound();
            }
            int result = dao.Delete(pagedata.id);
            return RedirectToAction("Money");
        }
        private void Initial_Items()
        {
            listItems.Add(new SelectListItem
            {
                Text = "請選擇",
                Value = "-1"
            });

            listItems.Add(new SelectListItem
            {
                Text = "收入",
                Value = "0"
            });
            listItems.Add(new SelectListItem
            {
                Text = "支出",
                Value = "1",
            });
            ViewBag.CategroyItems = listItems;
        }
    }
}