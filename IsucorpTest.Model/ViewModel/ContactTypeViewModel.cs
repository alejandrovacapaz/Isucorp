using IsucorpTest.Model.DBModel;

namespace IsucorpTest.Model.ViewModel
{
    public class ContactTypeViewModel
    {
        public ContactTypeViewModel()
        {

        }

        public ContactTypeViewModel(ContactType contact)
        {
            Id = contact.Id;
            Name = contact.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
