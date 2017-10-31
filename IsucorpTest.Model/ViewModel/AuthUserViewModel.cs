using IsucorpTest.Model.DBModel;

namespace IsucorpTest.Model.ViewModel
{
    public class AuthUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool? Enabled { get; set; }

        public AuthUserViewModel()
        { }

        public AuthUserViewModel(AuthUser user)

        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Enabled = user.AdminEnabled;
        }
    }
}
