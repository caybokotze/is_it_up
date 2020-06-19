using System;

namespace IsItUpOrDown
{
    class NotificationHandler
    {
        public delegate void RegisterNotificationDelegate();

        public RegisterNotificationDelegate RegisterNotificationDelegateInstance;

        public void RegisterNotificationHandler()
        {
            Console.WriteLine("Notification handler has been registered.");
            if (RegisterNotificationDelegateInstance != null)
            {
                RegisterNotificationDelegateInstance();
            }
        }

        public static void Send(WebsiteError error)
        {
            var registerNotification = new NotificationHandler();
            var emailNotification = new TelegramNotification(error);
            registerNotification.RegisterNotificationDelegateInstance += emailNotification.OnNotificationSent;
            registerNotification.RegisterNotificationHandler();
        }
    }
}