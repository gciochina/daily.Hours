using System.Web.Mvc;
using Daily.Hours.Web.Models;

namespace Daily.Hours.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            User viewModel = null;
            if (this.User.Identity.IsAuthenticated)
            {
                viewModel = new User { EmailAddress = this.User.Identity.Name };
            }
            return View(viewModel);
        }
    }
}
