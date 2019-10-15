using System.Web.Mvc;

namespace DogDayCareRS.MVC.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }

        
        public ActionResult Services()
        {

            return View();
        }
    }
}
