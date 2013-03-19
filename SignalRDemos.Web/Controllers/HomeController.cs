using System.Web.Mvc;
using NServiceBus;

namespace SignalRDemos.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public IBus Bus { get; set; }

        public ActionResult Index()
        {

            return View();
        }

    }
}
