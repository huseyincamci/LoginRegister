using LoginRegisterFromScratch.Models;
using System.Linq;
using System.Web.Mvc;

namespace LoginRegisterFromScratch.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //[OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            using (LoginRegisterDbContex context = new LoginRegisterDbContex())
            {
                var users = context.Users.OrderByDescending(x=> x.CreatedDateTime).ToList();
                return View(users);
            }
        }
    }
}