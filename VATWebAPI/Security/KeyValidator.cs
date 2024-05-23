using System.Configuration;

namespace VATWebAPI.Security
{
    public class KeyValidator
    {
        public bool Validate(string userKey)
        {
            string applicationKey = ConfigurationManager.AppSettings["apiKey"];

            return applicationKey == userKey;
        }
    }
}