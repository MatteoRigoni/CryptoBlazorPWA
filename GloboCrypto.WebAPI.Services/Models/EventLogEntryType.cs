using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Models
{
    public enum EventLogEntryType
    {
        Authenticate,
        Subscription,
        SubscriptionUpdate,
        Notification,
        Information,
        Error
    }
}
