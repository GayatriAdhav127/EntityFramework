using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AssignColor.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Serialization;

namespace AssignColor.Controllers
{
    public class TestController : Controller
    {
        CompanyContext cc = new CompanyContext();
        // GET: Test
        public ActionResult Index()
        {

            var v = from t in cc.Products
                    select new VMColor
                    {
                        ProductID = t.ProductID,
                        ProductName = t.ProductName,
                        ColorCount = t.ProductColors.Count()
                    };

            return View(v.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product rec, Int64[] chk)
        {
            this.cc.Products.Add(rec);
            this.cc.SaveChanges();
            foreach (Int64 cid in chk)
            {
                ProductColor temp = new ProductColor();
                temp.ColorID = cid;
                temp.ProductID = rec.ProductID;
                this.cc.ProductColors.Add(temp);
            }
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public ActionResult GetCheckBox()
        {
            var v = from t in cc.Colors
                    select new CheckBoxVM
                    {
                        Value = t.ColorID,
                        Text = t.ColorName,
                        ISelected = false
                    };
            return View("_CheckBoxList", v.ToList());
        }
        public ActionResult GetChecked(Int64 Id)
        {
        var rec = this.cc.Products.Find(Id);
            var c = rec.ProductColors.Select(a => a.ColorID).ToList();
            var v = from t in cc.Colors
                    select new CheckBoxVM

                    {
                       Value=t.ColorID,
                       Text=t.ColorName,
                       ISelected=c.Contains(t.ColorID)
                    };
            ViewBag.chk = v.ToList();
            return View("_CheckBox", v.ToList());
        }
        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            var rec = this.cc.Products.Find(id);
            return View(rec);
        }
        [HttpPost]
        public ActionResult Edit(Product rec, Int64[] chk)
        {
            this.cc.Entry(rec).State = System.Data.Entity.EntityState.Modified;
            this.cc.SaveChanges();
            var pclr = this.cc.ProductColors.Where(p => p.ProductID == rec.ProductID).ToList();
            foreach(var c in pclr)
            {
                this.cc.ProductColors.Remove(c);
           }
            foreach(Int64 cid in chk)
            {
                ProductColor temp = new ProductColor();
                temp.ColorID = cid;
                temp.ProductID = rec.ProductID;
                this.cc.ProductColors.Add(temp);
            }
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }
         public ActionResult Details(Int64 id)
        {
            var rec=this.cc.Products.Find(id);
            return View(rec);
        }

        [HttpGet]
        public ActionResult Delete(Int64 id)
        {
           
            var rec = this.cc.Products.Find(id);
            this.cc.Products.Remove(rec);
            this.cc.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}