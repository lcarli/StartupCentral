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
            var startup = db.Startup.Include(s => s.Aceleradora).Include(s => s.Benefício).Include(s => s.Status);
            return View(await db.Startup.ToListAsync());
        }

        // GET: Startupbs/Details/5
        public async Task<ActionResult> Details(Guid? id)
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
            //preenchendo Beneficio
            var query = from b in db.Benefício
                        select b;
            ViewBag.BeneficioList = new SelectList(query, "ID", "Nome");

            //preenchendo Status
            var query2 = from s in db.Status
                         select s;

            ViewBag.StatusList = new SelectList(query2, "ID", "Nome");

            //preenchendo Aceleradora
            var query3 = from a in db.Aceleradora
                         select a;

            ViewBag.AceleradoraList = new SelectList(query3, "ID", "Nome");

            return View();
        }

        // POST: Startupbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Startupbs startupbs)
        {
            Guid i = new Guid(ModelState["Benefício"].Value.AttemptedValue);
            startupbs.Benefício = (from b in db.Benefício where b.ID == i select b).FirstOrDefault();
            Guid ii = new Guid(ModelState["Status"].Value.AttemptedValue);
            startupbs.Status = (from s in db.Status where s.ID == ii select s).FirstOrDefault();
            Guid iii = new Guid(ModelState["Aceleradora"].Value.AttemptedValue);
            startupbs.Aceleradora = (from a in db.Aceleradora where a.ID == iii select a).FirstOrDefault();
            if (CustomValidateModel(startupbs))
            {
                startupbs.ID = Guid.NewGuid();
                db.Startup.Add(startupbs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(startupbs);
        }

        private bool CustomValidateModel(Startupbs s)
        {
            if (s.Nome == "" || s.MicrosoftAccount == "" || s.Email == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // GET: Startupbs/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
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
            ViewBag.BeneficioList = new SelectList(db.Benefício, "ID", "Nome",startupbs.Benefício.ID);
            ViewBag.StatusList = new SelectList(db.Status, "ID", "Nome", startupbs.Status.ID);
            ViewBag.AceleradoraList = new SelectList(db.Aceleradora, "ID", "nome", startupbs.Aceleradora.ID);
            return View(startupbs);
        }

        // POST: Startupbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Startupbs startupbs)
        {
            Guid i = new Guid(ModelState["Benefício.ID"].Value.AttemptedValue);
            startupbs.Benefício = (from b in db.Benefício where b.ID == i select b).FirstOrDefault();
            Guid ii = new Guid(ModelState["Status.ID"].Value.AttemptedValue);
            startupbs.Status = (from s in db.Status where s.ID == ii select s).FirstOrDefault();
            Guid iii = new Guid(ModelState["Aceleradora.ID"].Value.AttemptedValue);
            startupbs.Aceleradora = (from a in db.Aceleradora where a.ID == iii select a).FirstOrDefault();
            if (CustomValidateModel(startupbs))
            {
                db.Entry(startupbs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(startupbs);
        }

        // GET: Startupbs/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
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
        public async Task<ActionResult> DeleteConfirmed(Guid id)
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
