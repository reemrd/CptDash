using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CptDash;
using CptDash.Models;

namespace CptDash.Controllers
{
    public class ReportSearchController : Controller
    {
        private CptDashEntities db = new CptDashEntities();

        // GET: /ReportSearch/
        public ActionResult Index()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name");
            //shows only the current month reports
            string month = "2014" + DateTime.Now.Month.ToString("d2");
            var reports = db.Reports.Include(r => r.Employee).Include(r => r.Period).Where(r => r.Period.Name == month);
            return View(reports.ToList());
        }

        // POST: /ReportSearch/
        [HttpPost]
        public ActionResult Index(int periodId, int employeeId)
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name");
            var reports = db.Reports.Where(r => r.EmployeeId == employeeId && r.PeriodId == periodId);
            return View(reports);
        }


        //Popup Report Window
        [HttpGet]
        public PartialViewResult ReportPopup(int id, int period)
        {

            var dailypayments = db.DailyPayments.Include(d => d.Category).Include(d => d.Report).Where(d => d.Report.EmployeeId == id && d.ReportId == period); 

            return PartialView("ReportPartial", dailypayments.ToList());
        }






        // GET: /ReportSearch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // GET: /ReportSearch/Create
        public PartialViewResult SearchFilters()
        { 
            var model = new ReportSearchViewModel{
            Periods = db.Periods.ToList(),
            Employees = db.Employees.ToList(),
        };
            return PartialView("SearchFilters", model);
        }

        // POST: /ReportSearch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchFilters([Bind(Include = "Id,PeriodId,EmployeeId")] Report report)
        {
            var model = new ReportSearchViewModel
            {
                Periods = db.Periods.ToList(),
                Employees = db.Employees.ToList(),
            };
            return PartialView("SearchFilters", model);
        }

        // GET: /ReportSearch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", report.EmployeeId);
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", report.PeriodId);
            return View(report);
        }

        // POST: /ReportSearch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PeriodId,EmployeeId")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", report.EmployeeId);
            ViewBag.PeriodId = new SelectList(db.Periods, "Id", "Name", report.PeriodId);
            return View(report);
        }

        // GET: /ReportSearch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: /ReportSearch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
