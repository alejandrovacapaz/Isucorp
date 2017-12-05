using System;
using System.ComponentModel.DataAnnotations;

namespace IsucorpTest.Model.DBModel
{
    public class Contact : BaseEntity
    {
        public Contact()
        {

        }
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }     
        public string Description { get; set; }
    }
}
