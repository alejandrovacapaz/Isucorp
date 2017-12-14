using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using System;
using System.Web;
using System.Web.Mvc;
using IsucorpTest.ViewModel.ViewModel;

namespace IsucorpTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactLogic _contactLogic;
        private readonly IContactTypeLogic _contactTypeLogic;

        public HomeController()
        {

        }

        public HomeController(IContactLogic contactLogic, IContactTypeLogic contactTypeLogic)
        {
            _contactLogic = contactLogic;
            _contactTypeLogic = contactTypeLogic;
        }

        public ActionResult Index()
        {
            var contacts = _contactLogic.GetAllContacts();
            return View(contacts);
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            var contact = new ContactViewModel();
            contact.ListContactTypes = _contactTypeLogic.GetAllContactTypes();
            return View(contact);
        }

        [HttpPost]       
        public ActionResult Add(ContactViewModel contact)
        {           
            contact.ContactType = _contactTypeLogic.GetContactType(contact.ContactTypeId);
            var result = _contactLogic.Add(contact);
            return Json(new { success = result });            
        }

        [HttpGet]
        public ActionResult Edit(int contactId)
        {
            var contact = _contactLogic.GetContactById(contactId);
            contact.ListContactTypes = _contactTypeLogic.GetAllContactTypes();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel contact)
        {
            contact.ContactType = _contactTypeLogic.GetContactType(contact.ContactTypeId);
            var result = _contactLogic.Update(contact);
            return Json(new { success = result });
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return PartialView("Delete");
        }

        [HttpPost]
        public ActionResult Delete(int contactId)
        {
            var result = _contactLogic.Delete(contactId);
            return Json(new { success = result });
        }

        [AllowAnonymous]
        public ActionResult LanguageEnglish()
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies["CultureInfo"] ?? new HttpCookie("CultureInfo");
            cookie.Value = "en-US";
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult LanguageSpanish()
        {
            var cookie = System.Web.HttpContext.Current.Request.Cookies["CultureInfo"] ?? new HttpCookie("CultureInfo");
            cookie.Value = "es-ES";
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);
            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return RedirectToAction("Index", "Home");
        }
    }
}