using IsucorpTest.Model.ViewModel;
using System.Collections.Generic;

namespace IsucorpTest.BLL.BuisnessLogic.Interfaces
{
    public interface IContactTypeLogic
    {
        List<ContactTypeViewModel> GetAllEntities();
    }
}
