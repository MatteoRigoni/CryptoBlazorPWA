using System.ComponentModel;

namespace GloboCrypto.PWA.Models
{
    public class LocalSettings
    {
        [Description("True to enable push notifications")]
        [DisplayName("Push Notifications")]
        public bool NotificationsEnabled { get; set; }
        [Description("True to enable dark mode")]
        [DisplayName("Dark Mode")]
        public bool DarkModeEnabled { get; set; }
        [Description("True if the app will refresh the data itself")]
        [DisplayName("Auto Refresh")]
        public bool AutoRefreshEnabled { get; set; }
        [Description("Interval between refreshes in seconds")]
        [DisplayName("Refresh Interval")]
        public long RefreshInterval { get; set; }
        [Description("The currency to use for the application")]
        [DisplayName("Currency")]
        public string Currency { get; set; }
        [Description("Interval for the price change period")]
        [DisplayName("Price Change Interval")]
        public string PriceChangeInterval { get; set; }
    }
}
