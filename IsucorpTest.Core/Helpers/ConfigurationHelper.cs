using System.Configuration;

namespace IsucorpTest.Core.Helpers
{
    public static class ConfigurationHelper
    {
        #region configuration_keys

        private const string Const_SendGridApiKey = "SendGridApiKey";
        private const string Const_SendGrid_TemplateEmailValidation = "SendGridTemplateEmailValidation";
        private const string Const_SendGrid_TemplatePasswordReset = "SendGridTemplatePasswordReset";

        private const string Const_EnabledEmailDomain = "EnabledEmailDomain";
        private const string Const_StorageBasePath = "StorageBasePath";

        private const string Const_WebBasePath = "WebBasePath";

        #endregion

        private static string GetParam(string paramKey)
        {
            return ConfigurationManager.AppSettings[paramKey];
        }

        public static string ParamStorageBasePath => GetParam(Const_StorageBasePath);

        public static string GetConnectionString(string paramKey)
        {
            return ConfigurationManager.ConnectionStrings[paramKey].ConnectionString;
        }

        #region SendGrid

        public static string SendGrid_ApiKey
        {
            get
            {
                return GetParam(Const_SendGridApiKey);
            }
        }

        public static string SendGrid_TemplateEmailValidation
        {
            get
            {
                return GetParam(Const_SendGrid_TemplateEmailValidation);
            }
        }

        public static string SendGrid_TemplatePasswordReset
        {
            get
            {
                return GetParam(Const_SendGrid_TemplatePasswordReset);
            }
        }

        #endregion

        #region General

        public static string EnabledEmailDomain
        {
            get
            {
                return GetParam(Const_EnabledEmailDomain);
            }
        }

        public static string Param_WebBasePath
        {
            get
            {
                return GetParam(Const_WebBasePath);
            }
        }
        #endregion
    }
}
