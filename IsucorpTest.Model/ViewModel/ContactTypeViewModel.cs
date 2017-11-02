using IsucorpTest.Model.DBModel;

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
        public string Name { get; set; }
    }
}
