﻿using System;
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
    public class ContatoController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        User user = HomeController.userlogged;

        // GET: Contato
        public async Task<ActionResult> Index()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View(await db.Contato.ToListAsync());
        }

        [HttpPost]
        //Overwrite with search
        public async Task<ActionResult> Index(string id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            var SearchString = Request.Form[0];
            var contatos = db.Contato.Where(s => s.Nome.Contains(SearchString));
            return View(await contatos.ToListAsync());
        }

        // GET: Contato/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = await db.Contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = contato.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
            await db.SaveChangesAsync();
            return View(contato);
        }

        // GET: Contato/Create
        public ActionResult Create()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            return View();
        }

        // POST: Contato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContatoId,Nome,Telefone,Email,TipoDoContato")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contato.Add(contato);
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = contato.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        // GET: Contato/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = await db.Contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContatoId,Nome,Telefone,Email,TipoDoContato")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contato).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = contato.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        // GET: Contato/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = await db.Contato.FindAsync(id);
            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contato contato = await db.Contato.FindAsync(id);
            db.Contato.Remove(contato);
            await db.SaveChangesAsync();
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = contato.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
