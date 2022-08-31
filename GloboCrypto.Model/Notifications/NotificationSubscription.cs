using System;
using System.Collections.Generic;
using System.Text;

namespace GloboCrypto.Model.Notifications
{
    public class NotificationSubscription
    {
        public string UserId { get; set; }
        public string Url { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
        public List<string> CoinIds { get; set; }
    }
}
