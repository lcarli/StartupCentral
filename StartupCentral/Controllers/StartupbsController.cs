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
    public class StartupbsController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        // GET: Startupbs
        public async Task<ActionResult> Index()
        {
            var startup = db.Startup.Include(s => s.Aceleradora).Include(s => s.Beneficio).Include(s => s.Status);
            return View(await startup.ToListAsync());
        }

        // GET: Startupbs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            return View(startupbs);
        }

        // GET: Startupbs/Create
        public ActionResult Create()
        {
            ViewBag.AceleradoraId = new SelectList(db.Aceleradora, "AceleradoraId", "Nome");
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome");
            return View();
        }

        // POST: Startupbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StartupbsId,Nome,Email,MicrosoftAccount,BizSparkID,BeneficioId,AceleradoraId,StatusId,ConsumoMes,ConsumoAcumulado,ConsumoPago,Observacoes,Owner")] Startupbs startupbs)
        {
            if (ModelState.IsValid)
            {
                db.Startup.Add(startupbs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AceleradoraId = new SelectList(db.Aceleradora, "AceleradoraId", "Nome", startupbs.AceleradoraId);
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", startupbs.BeneficioId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", startupbs.StatusId);
            return View(startupbs);
        }

        // GET: Startupbs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            ViewBag.AceleradoraId = new SelectList(db.Aceleradora, "AceleradoraId", "Nome", startupbs.AceleradoraId);
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", startupbs.BeneficioId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", startupbs.StatusId);
            return View(startupbs);
        }

        // POST: Startupbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StartupbsId,Nome,Email,MicrosoftAccount,BizSparkID,BeneficioId,AceleradoraId,StatusId,ConsumoMes,ConsumoAcumulado,ConsumoPago,Observacoes,Owner")] Startupbs startupbs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(startupbs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AceleradoraId = new SelectList(db.Aceleradora, "AceleradoraId", "Nome", startupbs.AceleradoraId);
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", startupbs.BeneficioId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Nome", startupbs.StatusId);
            return View(startupbs);
        }

        // GET: Startupbs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            return View(startupbs);
        }

        // POST: Startupbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Startupbs startupbs = await db.Startup.FindAsync(id);
            db.Startup.Remove(startupbs);
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
