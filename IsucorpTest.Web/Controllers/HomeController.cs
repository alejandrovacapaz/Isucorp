using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.Model.ViewModel;
using System.Web.Mvc;

namespace IsucorpTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactLogic _contactLogic;

        public HomeController()
        {

        }
        public HomeController(IContactLogic contactLogic)
        {
            _contactLogic = contactLogic;
        }

        public ActionResult Index()
        {
            var contacts = _contactLogic.GetAllContacts();
            return View(contacts);
        }

        public ActionResult Add(ContactViewModel contact)
        {
            _contactLogic.Add(contact);
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