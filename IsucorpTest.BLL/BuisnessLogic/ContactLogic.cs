using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IsucorpTest.BLL.BuisnessLogic
{
    public class ContactLogic : IContactLogic
    {
        private readonly ICollectionRepository<Contact> _contactRepository;

        public ContactLogic (ICollectionRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public bool Add (ContactViewModel contact)
        {
            try
            {
                return _contactRepository.Add(new Contact(contact)) > 0;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        public bool Update (ContactViewModel contact)
        {
            try
            {
                _contactRepository.Update(new Contact(contact));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete (int contactId)
        {
            try
            {
                _contactRepository.Delete(contactId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ContactViewModel> GetAllContacts()
        {
            try
            {
                return _contactRepository.GetAllEntities().Select(n => new ContactViewModel(n)).ToList();
            }
            catch (Exception)
            {
                return new List<ContactViewModel>();
            }
        }

        public ContactViewModel GetContactById(int contactId)
        {
            return new ContactViewModel(_contactRepository.FindById(contactId));
        }
    }
}

