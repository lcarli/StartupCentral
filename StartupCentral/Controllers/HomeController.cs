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
    }
}