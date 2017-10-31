using IsucorpTest.Model.DBModel;
using System;

namespace IsucorpTest.Model.ViewModel
{
    public class ContactViewModel
    {
        public ContactViewModel ()
        {

        }

        public ContactViewModel(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            PhoneNumber = contact.PhoneNumber;
            BirthDate = contact.BirthDate;
            ContactTypeId = contact.ContactTypeId;
            ContactType = contact.ContactType;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
    }
}
