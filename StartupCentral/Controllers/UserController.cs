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
    public class UserController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        User userlogged = HomeController.userlogged;

        // GET: User
        public async Task<ActionResult> Index()
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            return View(await db.User.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            ViewBag.Owner = true; // booleano para verificar se existe startups com esse user como Owner
            ViewBag.OwnerCounter = "20"; //contagem, em %, de owners desse user
            ViewBag.SiteCounter = "50"; //contagem, em %, de acesso deste user ao site
            ViewBag.CadastroCounter = "1"; //contagem, em %, de cadastros feitos por esse usuario.
            ViewBag.UserImage = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user;
            if (userlogged.UserId == id)
            {
                user = await db.User.FindAsync(id);
            }
            else
            {
                user = await db.User.FindAsync(userlogged.UserId);
            }
            
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,nome,email")] User user)
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,nome,email")] User user)
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.Nome = userlogged.nome.ToString();
            ViewBag.UserId = userlogged.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.User.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.User.FindAsync(id);
            db.User.Remove(user);
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
