using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.Model.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace IsucorpTest.BLL.BuisnessLogic
{
    public class ContactTypeLogic: IContactTypeLogic
    {
        private readonly ICollectionRepository<ContactType> _contactTypeRepository;

        public ContactTypeLogic(ICollectionRepository<ContactType> contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        public List<ContactTypeViewModel> GetAllEntities()
        {
            return _contactTypeRepository.GetAllEntities().Select(c => new ContactTypeViewModel(c)).ToList();
        }
    }
}
