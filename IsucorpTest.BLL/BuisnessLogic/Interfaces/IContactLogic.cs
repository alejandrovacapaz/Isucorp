using IsucorpTest.ViewModel;
using System.Collections.Generic;

namespace IsucorpTest.BLL.BuisnessLogic.Interfaces
{
    public interface IContactLogic
    {
        bool Add(ContactViewModel contact);
        bool Update(ContactViewModel contact);
        bool Delete(int contactId);
        List<ContactViewModel> GetAllContacts();
        ContactViewModel GetContactById(int contactId);
    }
}
