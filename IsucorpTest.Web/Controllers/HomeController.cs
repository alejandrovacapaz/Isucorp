﻿using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.Model.ViewModel;
using System.Web.Mvc;

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
    }
}