using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PensionMvc;
using PensionMvc.Models;

namespace PensionMvc.Controllers
{
    public class PensionerInputsController : Controller
    {
        private MvcDbContext db = new MvcDbContext();

        // GET: PensionerInputs
        public ActionResult Index()
        {
            return View(db.PensionerInputs.ToList());
        }

        // GET: PensionerInputs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionerInput pensionerInput = db.PensionerInputs.Find(id);
            if (pensionerInput == null)
            {
                return HttpNotFound();
            }
            return View(pensionerInput);
        }

        // GET: PensionerInputs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PensionerInputs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PAN,NamePensioner,DOB,AadhaarNo,PensionSelected")] PensionerInput pensionerInput)
        {
            if (ModelState.IsValid)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44305/");
                    //HTTP GET
                    var responseTask = client.GetAsync($"api/ProcessPension/{pensionerInput.PAN}");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var results = result.Content.ReadAsStringAsync().Result;
                        var pd = JsonConvert.DeserializeObject<PensionDetails>(results);
                        return RedirectToAction("Viewps", pd);

                    }
                    else //web api sent error response 
                    {
                        //log response status here..
                        return RedirectToAction("Index");
                    }

                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpGet]
            public ActionResult Viewps(PensionDetails pd)
            {

                return View(pd);

            }
        // GET: PensionerInputs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionerInput pensionerInput = db.PensionerInputs.Find(id);
            if (pensionerInput == null)
            {
                return HttpNotFound();
            }
            return View(pensionerInput);
        }

        // POST: PensionerInputs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PAN,NamePensioner,DOB,AadhaarNo,PensionSelected")] PensionerInput pensionerInput)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pensionerInput).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pensionerInput);
        }

        // GET: PensionerInputs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PensionerInput pensionerInput = db.PensionerInputs.Find(id);
            if (pensionerInput == null)
            {
                return HttpNotFound();
            }
            return View(pensionerInput);
        }

        // POST: PensionerInputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PensionerInput pensionerInput = db.PensionerInputs.Find(id);
            db.PensionerInputs.Remove(pensionerInput);
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
