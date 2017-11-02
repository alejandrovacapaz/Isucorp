using System.Collections.Generic;
using IsucorpTest.Model.ViewModel;

namespace IsucorpTest.BLL.BuisnessLogic.Interfaces
{
    public interface IContactTypeLogic
    {
        List<ContactTypeViewModel> GetAllContactTypes();
        ContactTypeViewModel GetContactType(int contactTypeId);
    }
}
