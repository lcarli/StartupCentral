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
    public class LogLoginController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        // GET: LogLogin
        public async Task<ActionResult> Index()
        {
            return View(await db.LogLogin.ToListAsync());
        }

        // GET: LogLogin/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogLogin logLogin = await db.LogLogin.FindAsync(id);
            if (logLogin == null)
            {
                return HttpNotFound();
            }
            return View(logLogin);
        }

        // GET: LogLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogLogin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,datetime")] LogLogin logLogin)
        {
            if (ModelState.IsValid)
            {
                logLogin.ID = Guid.NewGuid();
                db.LogLogin.Add(logLogin);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(logLogin);
        }

        // GET: LogLogin/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogLogin logLogin = await db.LogLogin.FindAsync(id);
            if (logLogin == null)
            {
                return HttpNotFound();
            }
            return View(logLogin);
        }

        // POST: LogLogin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,datetime")] LogLogin logLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logLogin).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(logLogin);
        }

        // GET: LogLogin/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogLogin logLogin = await db.LogLogin.FindAsync(id);
            if (logLogin == null)
            {
                return HttpNotFound();
            }
            return View(logLogin);
        }

        // POST: LogLogin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            LogLogin logLogin = await db.LogLogin.FindAsync(id);
            db.LogLogin.Remove(logLogin);
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
