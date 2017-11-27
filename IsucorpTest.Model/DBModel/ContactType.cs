namespace IsucorpTest.Model.DBModel
{
    public class ContactType : BaseEntity
    {
        public ContactType()
        {
            
        }

        //public ContactType(ContactTypeViewModel contactType)
        //{
        //    Id = contactType.Id;
        //    Name = contactType.Name;
        //}

        public string Name { get; set; }
    }
}
