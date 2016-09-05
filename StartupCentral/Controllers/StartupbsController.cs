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
            var startup = db.Startup.Include(s => s.Aceleradora).Include(s => s.Beneficio).Include(s => s.Status).Include(s=>s.Contatos);
            return View(await startup.ToListAsync());
        }

        // GET: Startupbs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            List<Contato> lc = (from c in db.Contato where c.Startup.Any(s => s.StartupbsId == id) select c).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            startupbs.Contatos = lc;
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = startupbs.Nome, UserId= HomeController.useridsession });
            await db.SaveChangesAsync();
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
        public async Task<ActionResult> Create([Bind(Include = "StartupbsId,Nome,Email,MicrosoftAccount,Contatos,BizSparkID,BeneficioId,AceleradoraId,StatusId,ConsumoMes,ConsumoAcumulado,ConsumoPago,Observacoes,Owner")] Startupbs startupbs)
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
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = startupbs.Nome, UserId= HomeController.useridsession });
            await db.SaveChangesAsync();
            return View(startupbs);
        }

        // GET: Startupbs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<Contato> lc = (from c in db.Contato where c.Startup.Any(s => s.StartupbsId == id) select c).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            startupbs.Contatos = lc;
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
        public async Task<ActionResult> Edit([Bind(Include = "StartupbsId,Nome,Email,MicrosoftAccount,BizSparkID,BeneficioId,Contatos,AceleradoraId,StatusId,ConsumoMes,ConsumoAcumulado,ConsumoPago,Observacoes,Owner")] Startupbs startupbs)
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
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = startupbs.Nome, UserId= HomeController.useridsession });
            await db.SaveChangesAsync();
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
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = startupbs.Nome, UserId= HomeController.useridsession });
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult UpdateSheet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update()
        {
            //O que fazer quando fizer o Upload?
            return View();
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
