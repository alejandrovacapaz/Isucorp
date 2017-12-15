using System.Collections.Generic;
using System.Linq;
using IsucorpTest.BLL.BuisnessLogic.Interfaces;
using IsucorpTest.DAL.Repositories.Interfaces;
using IsucorpTest.Model.DBModel;
using IsucorpTest.ViewModel;
using IsucorpTest.ViewModel.ViewModel;

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
            return _contactTypeRepository.List().Select(ct => new ContactTypeViewModel(ct)).ToList();
        }

        public ContactTypeViewModel GetContactType(int contactTypeId)
        {
            return new ContactTypeViewModel(_contactTypeRepository.FindById(contactTypeId));
        }
    }
}
