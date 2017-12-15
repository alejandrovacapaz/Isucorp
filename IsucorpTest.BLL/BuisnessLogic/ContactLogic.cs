using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IsucorpTest.ViewModel.ViewModel;

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
                var dueDateSplit = contact.BirthDateString.Split('/');
                contact.BirthDate = new DateTime(Convert.ToInt16(dueDateSplit[2]), Convert.ToInt16(dueDateSplit[0]), Convert.ToInt16(dueDateSplit[1]));
                var name = new SqlParameter("@Name", contact.Name);
                var phoneNumber = new SqlParameter("@PhoneNumber", contact.PhoneNumber?? "");
                var birthDate = new SqlParameter("@BirthDate", contact.BirthDate);
                var contactTypeId = new SqlParameter("@ContactTypeId", contact.ContactTypeId);
                var description = new SqlParameter("@Description", contact.Description);
                return _contactRepository.ExecuteStoredProcedure("dbo.sp_InsertContact @Name, @PhoneNumber" +
                    ", @BirthDate, @ContactTypeId, @Description", name, phoneNumber,
                    birthDate, contactTypeId, description);
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
                var dueDateSplit = contact.BirthDateString.Split('/');
                contact.BirthDate = new DateTime(Convert.ToInt16(dueDateSplit[2]), Convert.ToInt16(dueDateSplit[0]), Convert.ToInt16(dueDateSplit[1]));
                var id = new SqlParameter("@Id", contact.Id);
                var name = new SqlParameter("@Name", contact.Name);
                var phoneNumber = new SqlParameter("@PhoneNumber", contact.PhoneNumber?? "");
                var birthDate = new SqlParameter("@BirthDate", contact.BirthDate);
                var contactTypeId = new SqlParameter("@ContactTypeId", contact.ContactTypeId);
                var description = new SqlParameter("@Description", contact.Description);
                return _contactRepository.ExecuteStoredProcedure("dbo.sp_UpdateContact @Id, @Name, @PhoneNumber" +
                    ", @BirthDate, @ContactTypeId, @Description", id, name, phoneNumber,
                    birthDate, contactTypeId, description);       
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
                var id = new SqlParameter("@Id", contactId);
                return _contactRepository.ExecuteStoredProcedure("dbo.sp_DeleteContact @Id", id);
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
                return _contactRepository.List().Select(n => new ContactViewModel(n)).OrderBy(x => x.Name)
                    .ThenBy(x => x.PhoneNumber).ThenBy(x => x.BirthDate).ThenBy(x => x.ContactTypeId).ToList();
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

