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

        // GET: Money
        public ActionResult Money(MoneyViewModel pagedata)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
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
            string P_Id = pagedata.id;
            if (P_Id != null)
            {
                if (pagedata.status == 1)
                {
                    int records = dao.Update(pagedata);//確定儲存更新
                    return View();
                }
                else
                {
                    MoneyViewModel instance = dao.GetSingleRow(P_Id);//確定更新
                    if (instance == null)
                    {
                        return View();
                    }
                    instance.status = 1;
                    return View(instance);
                }
            }
            else
              if (pagedata.categoryyy != "-1" && pagedata.categoryyy != null)
            {
                int row = dao.Insert(pagedata);
                return View();
            }
            else
            {
                ModelState.Clear();
                return View();
            }

        }

        public ActionResult List(MoneyViewModel pagedata)
        {
            var AccountLists = dao.GetAllLists();
            return View(AccountLists);
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
    }
}