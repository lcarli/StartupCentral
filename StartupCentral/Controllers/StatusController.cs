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
    public class StatusController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        User user = HomeController.userlogged;

        // GET: Status
        public async Task<ActionResult> Index()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View(await db.Status.ToListAsync());
        }

        // GET: Status/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = await db.Status.FindAsync(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = status.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
            await db.SaveChangesAsync();
            return View(status);
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StatusId,Nome")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Status.Add(status);
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = status.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = await db.Status.FindAsync(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StatusId,Nome")] Status status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(status).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = status.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = await db.Status.FindAsync(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Status status = await db.Status.FindAsync(id);
            db.Status.Remove(status);
            await db.SaveChangesAsync();
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = status.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
