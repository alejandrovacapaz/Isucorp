using IsucorpTest.Model.ViewModel;
using System;

namespace IsucorpTest.Model.DBModel
{
    public class Contact : BaseEntity
    {
        public Contact()
        {

        }
        public Contact(ContactViewModel contact)
        {
            Name = contact.Name;
            PhoneNumber = contact.PhoneNumber;
            BirthDate = contact.BirthDate;
            ContactTypeId = contact.ContactTypeId;
            ContactType = contact.ContactType;
        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
    }
}
