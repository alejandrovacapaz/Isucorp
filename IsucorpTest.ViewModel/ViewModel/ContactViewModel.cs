using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IsucorpTest.Language.Entities;
using IsucorpTest.Model.DBModel;

namespace IsucorpTest.ViewModel.ViewModel
{
    public class ContactViewModel
    {
        public ContactViewModel ()
        {
            BirthDate = DateTime.Now;
            PhoneNumber = "";
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
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceName = "NameLengthError", ErrorMessageResourceType = typeof(ContactEntity))]
        [Display(Name = "Name", ResourceType = typeof(ContactEntity))]
        public string Name { get; set; }
        [StringLength(20, MinimumLength = 0, ErrorMessageResourceName = "PhoneNumberLength", ErrorMessageResourceType = typeof(ContactEntity))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(ContactEntity))]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "BirthDate", ResourceType = typeof(ContactEntity))]
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
