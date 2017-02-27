using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NCalc;
using CalcApp.Models;
using System;
using CalcApp.Helper;

namespace CalcApp.Controllers
{
    public class MainController : Controller
    {
        private CalculationContext db = new CalculationContext();
        private List<Item> items;       

        // GET: Main/Create
        public ActionResult Index()
        {
            ViewBag.ItemsList = db.Items.OrderByDescending(o => o.Id).Take(10).ToList();
            return View();            
        }

        // POST: Main/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Input,Result,Degree")] Item item)
        {
            ViewBag.ItemsList = db.Items.OrderByDescending(o => o.Id).Take(10).ToList();
            ViewBag.error = false;
            if (!String.IsNullOrEmpty(item.Input))
            {               
                string replace_input1 = item.Input.Replace(";", "_SK_");
                string replace_input2 = replace_input1.Replace(",", ".");
                string replace_input3 = replace_input2.Replace("_SK_", ",");

                Expression e = new Expression(replace_input3);

                // Extend Ncalc with custom parameters and extensions
                e.EvaluateParameter += NCalcFunctions.Parameters;
                e.EvaluateFunction += NCalcFunctions.Extensions;

                // Extend and override Ncalc functions for degree operations 
                if (item.Degree == true)
                e.EvaluateFunction += NCalcFunctions.ExtensionsDegree;

                if (e.HasErrors())
                {
                    ViewBag.error = true;
                    ViewBag.error_title = "HasErrors()";
                    ViewBag.error_desc = e.Error;
                    ViewBag.lastResult = item.Input;
                    return View(item);
                }
                else
                {
                    try
                    {
                        item.Result = e.Evaluate().ToString();                       
                    }
                    catch (Exception exc)
                    {
                        ViewBag.error = true;
                        ViewBag.error_title = "Exception";
                        ViewBag.error_desc = exc.Message;
                        ViewBag.lastResult = item.Input;
                        return View(item);
                    }
                }
            }

            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                ViewBag.ItemsList = db.Items.OrderByDescending(o => o.Id).Take(10).ToList();
                ViewBag.lastResult = item.Result;
                return View(item);
            }           
            return View();
        }       

        // GET: Main/Delete/5        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");            
        }

        // GET: Main/DeleteAll      
        public ActionResult DeleteAll()
        {
            items = db.Items.ToList();
            foreach (var item in items)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }              
    }
}
