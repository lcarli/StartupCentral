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
            Models.User user = new Models.User() { nome = cp.Claims.First(c => c.Type == "name").Value, email = cp.Name };
            ViewBag.Nome = user.nome.ToString();
            if (db.User.FindAsync(user.email) == null)
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
            GetTopProfiles();

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
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var result = String.Format("{0} milliseconds since 1970/01/01", ts.TotalMilliseconds);
            //Criar o data para o gráfico, aqui
            return Json(
                new[] { new[] { ts.TotalMilliseconds, randNum } },
                JsonRequestBehavior.AllowGet);
        }

        public void GetTopProfiles()
        {
            ViewBag.Top1 = "Portal Telemedicina";
            ViewBag.ConsumoTotalTop1 = "$4.500";
        }
    }
}