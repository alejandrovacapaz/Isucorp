using IsucorpTest.Model.DBModel;
using System;
using System.Collections.Generic;

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
            Description = contact.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }
        public string Description { get; set; }
        public List<ContactTypeViewModel> ContactTypes { get; set; }
}
}
