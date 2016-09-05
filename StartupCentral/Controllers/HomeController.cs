using StartupCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace StartupCentral.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private StartupDBContext db = new StartupDBContext();
        public static int useridsession;

        public async Task<ActionResult> Index()
        {
            //GetUser
            var cp = ClaimsPrincipal.Current.Identities.First();
            //User users = new Models.User() { nome = cp.Claims.First(c => c.Type == "name").Value, email = cp.Name };
            User user = db.User.Where(u => u.email == cp.Name).FirstOrDefault();

            //se user for null, usuario nao pode acessar.
            if (user == null)
            {
                
            }
            else
            {
                ViewBag.Nome = user.nome.ToString();
                db.LogLogin.Add(new Models.LogLogin() { datetime = DateTime.Now, user = user });
                await db.SaveChangesAsync();
                ViewBag.UserId = user.UserId;
                useridsession = user.UserId;
                //Edit Menu
                //if (user.RoleId == 3 || user.RoleId == 4)
                //{

                //}

                var ht = HttpContext.User;

                //Complete Dashboards
                countStartupsBS();
                countStartupsBSPlus();
                countStartupsNews();
                SumConsumoTotal();
                GetTopProfiles();
            }
            return View();
        }

        public void countStartupsBS()
        {
            var v1 = (from s in db.Startup where s.Beneficio.Nome == "BizSpark PLUS" select s).ToList().Count();
            var v2 = (from s in db.Startup where s.Beneficio.Nome == "BizSpark PLUS" select s).ToList().Count();
            var value = v1 + v2;
            ViewBag.NumStartupsBS = value;
        }

        public void countStartupsBSPlus()
        {
            var value = (from s in db.Startup where s.Beneficio.Nome == "BizSpark" select s).ToList().Count();
            @ViewBag.NumStartupsBSPlus = value;
        }

        public void countStartupsNews()
        {
            var value = (from s in db.Startup where s.Status.Nome == "Não Inscrito" select s).ToList().Count();

            @ViewBag.NumNews = value;
        }
        public void SumConsumoTotal()
        {
            var v = (from s in db.Startup select s).ToList();
            decimal value = 0;
            foreach (var item in v)
            {
                value = value + item.ConsumoAcumulado;
            }
            @ViewBag.ConsumoTotal = $"${value}";
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
            var v = (from s in db.Startup orderby s.ConsumoAcumulado descending select s).ToList();
            if (v.Count() > 4)
            {
                ViewBag.Top1 = v[0].Nome;
                ViewBag.ConsumoTotalTop1 = $"${v[0].ConsumoAcumulado}";
                ViewBag.Top2 = v[1].Nome;
                ViewBag.ConsumoTotalTop2 = $"${v[1].ConsumoAcumulado}";
                ViewBag.Top3 = v[2].Nome;
                ViewBag.ConsumoTotalTop3 = $"${v[2].ConsumoAcumulado}";
                ViewBag.Top4 = v[3].Nome;
                ViewBag.ConsumoTotalTop4 = $"${v[3].ConsumoAcumulado}";
                ViewBag.Top5 = v[4].Nome;
                ViewBag.ConsumoTotalTop5 = $"${v[4].ConsumoAcumulado}";
            }
            else if (v.Count() > 0)
            {
                ViewBag.Top1 = v[0].Nome;
                ViewBag.ConsumoTotalTop1 = $"${v[0].ConsumoAcumulado}";
            }
            else { }
        }
    }
}