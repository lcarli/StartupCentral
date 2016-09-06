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

        // GET: Aceleradora
        public async Task<ActionResult> Index()
        {
            var aceleradora = db.Aceleradora.Include(a => a.Beneficio).Include(a=>a.Endereco).Include(a=>a.Contatos).Include(a=>a.Startups);
            return View(await aceleradora.ToListAsync());
        }

        // GET: Aceleradora/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            List<Contato> lc = (from c in db.Contato where c.Startup.Any(s => s.StartupbsId == id) select c).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            aceleradora.Contatos = lc;
            if (aceleradora == null)
            {
                return HttpNotFound();
            }
            //db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = aceleradora.Nome, UserId= HomeController.useridsession });
            //await db.SaveChangesAsync();
            return View(aceleradora);
        }

        // GET: Aceleradora/Create
        public ActionResult Create()
        {
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome");
            return View();
        }

        // POST: Aceleradora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AceleradoraId,Nome,BeneficioId,Observacoes,Endereco,Contatos")] Aceleradora aceleradora)
        {
            if (ModelState.IsValid)
            {
                db.Aceleradora.Add(aceleradora);
                await db.SaveChangesAsync();
                //db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = aceleradora.Nome, UserId = HomeController.useridsession });
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", aceleradora.BeneficioId);
            return View(aceleradora);
        }

        // GET: Aceleradora/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<Contato> lc = (from c in db.Contato where c.Startup.Any(s => s.StartupbsId == id) select c).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            aceleradora.Contatos = lc;
            if (aceleradora == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", aceleradora.BeneficioId);
            return View(aceleradora);
        }

        // POST: Aceleradora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AceleradoraId,Nome,BeneficioId,Observacoes,Endereco,Contatos")] Aceleradora aceleradora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aceleradora).State = EntityState.Modified;
                await db.SaveChangesAsync();
                //db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = aceleradora.Nome, UserId = HomeController.useridsession });
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficioId = new SelectList(db.Benefício, "BeneficioId", "Nome", aceleradora.BeneficioId);
            return View(aceleradora);
        }

        // GET: Aceleradora/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Aceleradora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aceleradora aceleradora = await db.Aceleradora.FindAsync(id);
            db.Aceleradora.Remove(aceleradora);
            await db.SaveChangesAsync();
            //db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = aceleradora.Nome, UserId= HomeController.useridsession });
            //await db.SaveChangesAsync();
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
