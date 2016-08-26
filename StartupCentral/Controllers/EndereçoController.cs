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
    [Authorize]
    public class EndereçoController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        // GET: Endereço
        public async Task<ActionResult> Index()
        {
            return View(await db.Endereço.ToListAsync());
        }

        // GET: Endereço/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereço endereço = await db.Endereço.FindAsync(id);
            if (endereço == null)
            {
                return HttpNotFound();
            }
            return View(endereço);
        }

        // GET: Endereço/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Endereço/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,rua,numero,complemento,cep,estado,país")] Endereço endereço)
        {
            if (ModelState.IsValid)
            {
                endereço.ID = Guid.NewGuid();
                db.Endereço.Add(endereço);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(endereço);
        }

        // GET: Endereço/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereço endereço = await db.Endereço.FindAsync(id);
            if (endereço == null)
            {
                return HttpNotFound();
            }
            return View(endereço);
        }

        // POST: Endereço/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,rua,numero,complemento,cep,estado,país")] Endereço endereço)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereço).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(endereço);
        }

        // GET: Endereço/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereço endereço = await db.Endereço.FindAsync(id);
            if (endereço == null)
            {
                return HttpNotFound();
            }
            return View(endereço);
        }

        // POST: Endereço/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Endereço endereço = await db.Endereço.FindAsync(id);
            db.Endereço.Remove(endereço);
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
