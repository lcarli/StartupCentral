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
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.ComponentModel;

namespace StartupCentral.Controllers
{
    public class StartupbsController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        User user = HomeController.userlogged;

        // GET: Startupbs
        public async Task<ActionResult> Index()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            var startup = db.Startup.Include(s => s.Aceleradora).Include(s => s.Beneficio).Include(s => s.Status).Include(s=>s.Contatos);
            return View(await startup.ToListAsync());
        }

        [HttpPost]
        //Overwrite with search
        public async Task<ActionResult> Index(string id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            var SearchString = Request.Form[0];
            var startups = db.Startup.Include(s => s.Aceleradora).Include(s => s.Beneficio).Include(s => s.Status).Include(s => s.Contatos).Where(s=>s.Nome.Contains(SearchString));
            return View(await startups.ToListAsync());
        }

        // GET: Startupbs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            List<Contato> lc = (from c in db.Contato where c.Startup.Any(s => s.StartupbsId == id) select c).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Startupbs startupbs = await db.Startup.FindAsync(id);
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Consultou, ObjectUsed = startupbs.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
            await db.SaveChangesAsync();
            startupbs.Contatos = lc;
            if (startupbs == null)
            {
                return HttpNotFound();
            }
            return View(startupbs);
        }

        // GET: Startupbs/Create
        public ActionResult Create()
        {
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
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
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (ModelState.IsValid)
            {
                db.Startup.Add(startupbs);
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Salvou, ObjectUsed = startupbs.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
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
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
            if (ModelState.IsValid)
            {
                db.Entry(startupbs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Editou, ObjectUsed = startupbs.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;
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
            db.GeneralLogs.Add(new GeneralLog { Datetime = DateTime.Now, Action = UserAction.Deletou, ObjectUsed = startupbs.Nome, UserId = db.User.Where(u => u.email == user.email).FirstOrDefault().UserId });
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
            ViewBag.Nome = user.nome.ToString();
            ViewBag.UserId = user.UserId;

            var file = Request.Files[0];
            var fileName = Path.GetFileName(file.FileName);

            Excel.Workbook MyBook = null;
            Excel.Application MyApp = null;
            Excel.Worksheet MySheet = null;
            MyApp = new Excel.Application();
            MyApp.Visible = false;
            MyBook = MyApp.Workbooks.Open(fileName);
            MySheet = (Excel.Worksheet)MyBook.Sheets[1];
            var lastRow = MySheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;

            
            BindingList<Startupbs> EmpList = new BindingList<Startupbs>();
            for (int index = 2; index <= lastRow; index++)
            {
                Array MyValues = (Array)MySheet.get_Range("A" +
                   index.ToString(), "D" + index.ToString()).Cells.Value;
                var stat = MyValues.GetValue(1, 3).ToString();
                int statID = 3;
                switch (stat)
                {
                    case "Active":
                        statID = 3;
                        break;
                    case "Declined":
                        statID = 12;
                        break;
                    case "Deleted":
                        statID = 9;
                        break;
                    case "Graduated":
                        statID = 10;
                        break;
                    case "Suspended":
                        statID = 11;
                        break;
                    default:
                        break;
                }
                EmpList.Add(new Startupbs
                {
                    Nome = MyValues.GetValue(1, 1).ToString(),
                    BizSparkID = MyValues.GetValue(1, 2).ToString(),
                    StatusId = statID,
                    ConsumoAcumulado = Convert.ToDecimal(MyValues.GetValue(1, 4))
                });
            }

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
