using System.Collections.Generic;
using System.Linq;
using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.Model.ViewModel;

namespace IsucorpTest.BLL.BuisnessLogic
{
    public class ContactTypeLogic : IContactTypeLogic
    {
        private readonly ICollectionRepository<ContactType> _contactTypeRepository;

        public ContactTypeLogic(ICollectionRepository<ContactType> contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        public List<ContactTypeViewModel> GetAllContactTypes()
        {
            return _contactTypeRepository.GetAllEntities().Select(ct => new ContactTypeViewModel(ct)).ToList();
        }             
    }
}
