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
            Id = contact.Id;
            Name = contact.Name;
            PhoneNumber = contact.PhoneNumber;
            ContactTypeId = contact.ContactTypeId;
            Description = contact.Description;
            var dueDateSplit = contact.BirthDateString.Split('/');
            BirthDate = new DateTime(Convert.ToInt16(dueDateSplit[2]), Convert.ToInt16(dueDateSplit[0]), Convert.ToInt16(dueDateSplit[1]));
        }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }     
        public string Description { get; set; }
    }
}
