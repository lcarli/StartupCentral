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
using System.Security.Claims;

namespace StartupCentral.Controllers
{
    public class BeneficioController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        User user = HomeController.userlogged;

        // GET: Beneficio
        public async Task<ActionResult> Index()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View(await db.Benefício.ToListAsync());
        }

        // GET: Beneficio/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = await db.Benefício.FindAsync(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = beneficio.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
            await db.SaveChangesAsync();
            return View(beneficio);
        }

        // GET: Beneficio/Create
        public ActionResult Create()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View();
        }

        // POST: Beneficio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BeneficioId,Nome")] Beneficio beneficio)
        {
            if (ModelState.IsValid)
            {
                db.Benefício.Add(beneficio);
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = beneficio.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        // GET: Beneficio/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = await db.Benefício.FindAsync(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            return View(beneficio);
        }

        // POST: Beneficio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BeneficioId,Nome")] Beneficio beneficio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = beneficio.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(beneficio);
        }

        // GET: Beneficio/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beneficio beneficio = await db.Benefício.FindAsync(id);
            if (beneficio == null)
            {
                return HttpNotFound();
            }
            return View(beneficio);
        }

        // POST: Beneficio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Beneficio beneficio = await db.Benefício.FindAsync(id);
            db.Benefício.Remove(beneficio);
            await db.SaveChangesAsync();
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = beneficio.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
