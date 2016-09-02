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
            var query = from b in db.Benefício
                        select b;

            var BList = new SelectList(query, "ID", "Nome");
            List<SelectListItem> sl = new List<SelectListItem>();
            foreach (var item in BList.Items)
            {
                var i = item as Benefício;
                sl.Add(new SelectListItem { Text = i.Nome, Value = i.ID.ToString() });
            }
            ViewBag.BeneficioList = sl;

            //preenchendo Status
            var query2 = from s in db.Status
                         select s;

            var SList = new SelectList(query2, "ID", "Nome");
            List<SelectListItem> sl2 = new List<SelectListItem>();
            foreach (var item in SList.Items)
            {
                var i = item as Status;
                sl2.Add(new SelectListItem { Text = i.Nome, Value = i.ID.ToString() });
            }
            ViewBag.StatusList = sl2;

            return View();
        }

        // POST: Startupbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nome,Email,MicrosoftAccount,BizSparkID,Benefício,Observação,Status,Contatos,ConsumoMes,ConsumoAcumulado,ConsumoPago")] Startupbs startupbs)
        {
            //preenchendo Beneficio
            var query = from b in db.Benefício
                        select b;

            var BList = new SelectList(query, "ID", "Nome");
            List<SelectListItem> sl = new List<SelectListItem>();
            foreach (var item in BList.Items)
            {
                var i = item as Benefício;
                sl.Add(new SelectListItem { Text = i.Nome, Value = i.ID.ToString() });
            }
            ViewBag.BeneficioList = sl;

            //preenchendo Status
            var query2 = from s in db.Status
                         select s;

            var SList = new SelectList(query2, "ID", "Nome");
            List<SelectListItem> sl2 = new List<SelectListItem>();
            foreach (var item in SList.Items)
            {
                var i = item as Status;
                sl2.Add(new SelectListItem { Text = i.Nome, Value = i.ID.ToString() });
            }
            ViewBag.StatusList = sl2;
           startupbs.Benefício = (from t in db.Benefício where t.ID.ToString() == ModelState["Benefício"].Value.AttemptedValue select t).First();

            if (ModelState.IsValid)
            {
                startupbs.ID = Guid.NewGuid();
                db.Startup.Add(startupbs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(startupbs);
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
            return View(startupbs);
        }

        // POST: Startupbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nome,Email,MicrosoftAccount,BizSparkID,ConsumoMes,ConsumoAcumulado,ConsumoPago")] Startupbs startupbs)
        {
            if (ModelState.IsValid)
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
