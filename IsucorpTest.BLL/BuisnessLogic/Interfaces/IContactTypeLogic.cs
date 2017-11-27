using System.Collections.Generic;
using IsucorpTest.ViewModel;

namespace IsucorpTest.BLL.BuisnessLogic.Interfaces
{
    public interface IContactTypeLogic
    {
        List<ContactTypeViewModel> GetAllContactTypes();
        ContactTypeViewModel GetContactType(int contactTypeId);
    }
}
