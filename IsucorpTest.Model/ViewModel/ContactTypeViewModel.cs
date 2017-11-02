using IsucorpTest.Model.DBModel;
using System.ComponentModel.DataAnnotations;

namespace IsucorpTest.Model.ViewModel
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
        [Display(Name = "Contact Type")]
        public string Name { get; set; }
    }
}
