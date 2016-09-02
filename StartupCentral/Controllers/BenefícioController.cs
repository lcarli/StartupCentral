using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StartupCentral.Models;

namespace StartupCentral.Controllers
{
    public class BenefícioController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        // GET: Benefício
        public async Task<ActionResult> Index()
        {
            return View(await db.Benefício.ToListAsync());
        }

        // GET: Benefício/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefício benefício = await db.Benefício.FindAsync(id);
            if (benefício == null)
            {
                return HttpNotFound();
            }
            return View(benefício);
        }

        // GET: Benefício/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Benefício/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome")] Benefício benefício)
        {
            if (ModelState.IsValid)
            {
                benefício.ID = Guid.NewGuid();
                db.Benefício.Add(benefício);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(benefício);
        }

        // GET: Benefício/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefício benefício = await db.Benefício.FindAsync(id);
            if (benefício == null)
            {
                return HttpNotFound();
            }
            return View(benefício);
        }

        // POST: Benefício/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome")] Benefício benefício)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benefício).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(benefício);
        }

        // GET: Benefício/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benefício benefício = await db.Benefício.FindAsync(id);
            if (benefício == null)
            {
                return HttpNotFound();
            }
            return View(benefício);
        }

        // POST: Benefício/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Benefício benefício = await db.Benefício.FindAsync(id);
            db.Benefício.Remove(benefício);
            await db.SaveChangesAsync();
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
