using System.ComponentModel.DataAnnotations;
using IsucorpTest.Language.Entities;
using IsucorpTest.Model.DBModel;

namespace IsucorpTest.ViewModel.ViewModel
{
    public class ContactTypeViewModel
    {
        public ContactTypeViewModel()
        {
            
        }

        public ContactTypeViewModel(ContactType contactType)
        {
            Id = contactType.Id;
            Name = contactType.Name;
        }      

        public int Id { get; set; }
        [Display(Name = "ContactType", ResourceType = typeof(ContactTypeEntity))]
        public string Name { get; set; }
    }
}
