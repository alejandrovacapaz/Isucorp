namespace IsucorpTest.Core
{
    public class Constants
    {
        public const string Claim_BaseUserId = "";
        public const string Claim_OAuthUserId = "";
        public const string Claim_OAuthUserMail = "";

        public const string RoleName_Admin = "";
        public const string RoleName_User = "";

        public const string DefaultAdmin_Email = "";
        public const string DefaultAdmin_Password = "";

        public const string SendGrid_RecipientName = "";
        public const string SendGrid_EmailLinkUrl = "";

        public const string SendGrid_RegisterInternalEmail = "";
        public const string SendGrid_RegisterEmailSubject = "";
        public const string SendGrid_RegisterEmailBody = "<html><p>Hello, -userName-! <br />Your registration is almost complete! Please Follow this link: <br /><a href='-linkUrl-'>Validate Email</a></p></html>";

        public const string SendGridAuthLink_ConfirmEmail = "?userID={0}&code={1}";
        public const string SendGridAuthLink_ResetPassword = "?userID={0}&code={1}";

        public const string ApiResultErrors_InvalidUser = "Invalid user";

        #region OAuth_Password_Links

        public const string OAuthLink_ConfirmEmail = "Page/Register/ConfirmEmail?userID={0}&code={1}";
        public const string OAuthLink_ResetPassword = "Page/resetPassword?userID={0}&ptoken={1}";
        public const string OAuthLink_ConfirmEmailAndResetPassword = "Page/resetPassword?userID={0}&ptoken={1}&etoken={2}";

        #endregion
    }
}
