using IsucorpTest.Model.DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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
            ContactType = new ContactTypeViewModel(contact.ContactType);
            Description = contact.Description;
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
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactTypeViewModel ContactType { get; set; }
        public List<ContactTypeViewModel> ListContactTypes { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string BirthDateString { get; set; }
    }
}
