using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CptDash;

namespace CptDash.Controllers
{
    public class Default1Controller : Controller
    {
        private CptDashEntities db = new CptDashEntities();

        // GET: /Default1/
        public ActionResult Index()
        {
            var dailypayments = db.DailyPayments.Include(d => d.Category).Include(d => d.Report);
            return View(dailypayments.ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPayment dailypayment = db.DailyPayments.Find(id);
            if (dailypayment == null)
            {
                return HttpNotFound();
            }
            return View(dailypayment);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.ReportId = new SelectList(db.Reports, "Id", "Id");
            return View();
        }

        // POST: /Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Amount,ReportId,CategoryId,Date")] DailyPayment dailypayment)
        {
            if (ModelState.IsValid)
            {
                db.DailyPayments.Add(dailypayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", dailypayment.CategoryId);
            ViewBag.ReportId = new SelectList(db.Reports, "Id", "Id", dailypayment.ReportId);
            return View(dailypayment);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPayment dailypayment = db.DailyPayments.Find(id);
            if (dailypayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", dailypayment.CategoryId);
            ViewBag.ReportId = new SelectList(db.Reports, "Id", "Id", dailypayment.ReportId);
            return View(dailypayment);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Amount,ReportId,CategoryId,Date")] DailyPayment dailypayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailypayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", dailypayment.CategoryId);
            ViewBag.ReportId = new SelectList(db.Reports, "Id", "Id", dailypayment.ReportId);
            return View(dailypayment);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPayment dailypayment = db.DailyPayments.Find(id);
            if (dailypayment == null)
            {
                return HttpNotFound();
            }
            return View(dailypayment);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyPayment dailypayment = db.DailyPayments.Find(id);
            db.DailyPayments.Remove(dailypayment);
            db.SaveChanges();
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
