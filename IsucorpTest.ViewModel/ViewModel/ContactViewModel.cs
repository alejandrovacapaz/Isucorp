using IsucorpTest.Model.DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IsucorpTest.ViewModel
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
            ContactType = new ContactTypeViewModel(contact.ContactType);
            Description = contact.Description;
        }

        public Contact GetContact()
        {
            var dueDateSplit = BirthDateString.Split('/');
            return new Contact
            {
                Id = Id,
                Name = Name,
                PhoneNumber = PhoneNumber,
                ContactTypeId = ContactTypeId,
                Description = Description,            
                BirthDate = new DateTime(Convert.ToInt16(dueDateSplit[2]), Convert.ToInt16(dueDateSplit[0]), Convert.ToInt16(dueDateSplit[1]))
            };
        }

        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 30 characters")]
        [Display(Name = "Contact Name")]
        public string Name { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessage = "Phone max lenght is 20 characters")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        [Required]
        public int ContactTypeId { get; set; }
        public virtual ContactTypeViewModel ContactType { get; set; }
        public List<ContactTypeViewModel> ListContactTypes { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string BirthDateString { get; set; }
    }
}
