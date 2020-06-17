using System;

namespace IsItUpOrDown
{
    class EmailNotification
    {
        private WebsiteError _websiteError;

        public EmailNotification(WebsiteError error)
        {
            _websiteError = error;
        }

        public string GetOutput()
        {
            return $"The website: '{_websiteError.WebsiteName}' has failed. \nThe error message is: {_websiteError.Error}";
        }
        public void OnNotificationSent()
        {
            Console.WriteLine(GetOutput());
        }
    }
}