using StartupCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StartupCentral.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private StartupDBContext db = new StartupDBContext();

        public async Task<ActionResult> Index()
        {
            //GetUser
            var cp = ClaimsPrincipal.Current.Identities.First();
            Models.User user = new Models.User() { ID = Guid.NewGuid(), nome = cp.Claims.First(c => c.Type == "name").Value, email = cp.Name };
            ViewBag.Nome = user.nome.ToString();
            if(db.User.FindAsync(user.email) ==  null)
            {
                db.User.Add(user);
                await db.SaveChangesAsync();
            }
            else
            {
                db.LogLogin.Add(new Models.LogLogin() { datetime = DateTime.Now, user = user });
            }

            //Complete Dashboards
            countStartupsBS();
            countStartupsBSPlus();
            countStartupsNews();
            SumConsumoTotal();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void countStartupsBS()
        {
            ViewBag.NumStartupsBS = "235";
        }

        public void countStartupsBSPlus()
        {
            @ViewBag.NumStartupsBSPlus = "18";
        }

        public void countStartupsNews()
        {
            @ViewBag.NumNews = "20";
        }
        public void SumConsumoTotal()
        {
            @ViewBag.ConsumoTotal = "$20.000";
        }

        public ActionResult JsonValues()
        {
            Random randNums = new Random();
            var randNum = randNums.Next(10, 20);
            return Json(
                new[] { new[] { new DateTime().Day, randNum } },
                JsonRequestBehavior.AllowGet);
        }
    }
}