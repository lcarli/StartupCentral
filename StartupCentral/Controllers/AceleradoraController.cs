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
    public class AceleradoraController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        // GET: Aceleradoras
        public async Task<ActionResult> Index()
        {
            return View(await db.Aceleradora.ToListAsync());
        }

        // GET: Aceleradoras/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            if (aceleradora == null)
            {
                return HttpNotFound();
            }
            return View(aceleradora);
        }

        // GET: Aceleradoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aceleradoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,nome")] Aceleradora aceleradora)
        {
            if (ModelState.IsValid)
            {
                aceleradora.ID = Guid.NewGuid();
                db.Aceleradora.Add(aceleradora);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aceleradora);
        }

        // GET: Aceleradoras/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            if (aceleradora == null)
            {
                return HttpNotFound();
            }
            return View(aceleradora);
        }

        // POST: Aceleradoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,nome")] Aceleradora aceleradora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aceleradora).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(aceleradora);
        }

        // GET: Aceleradoras/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            if (aceleradora == null)
            {
                return HttpNotFound();
            }
            return View(aceleradora);
        }

        // POST: Aceleradoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            db.Aceleradora.Remove(aceleradora);
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
