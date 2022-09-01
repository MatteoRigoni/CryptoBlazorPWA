namespace GloboCrypto.PWA.Extensions
{
    public static class TimeFormattingExtensions
    {
        /// <summary>
        /// Converts a long representing seconds to a formatted string
        /// </summary>
        /// <param name="source">number in seconds</param>
        /// <param name="sigValues">number of significant values to include (0 = all)</param>
        /// <returns>formatted version of the duration e.g. 10m 30s</returns>
        public static string ToDuration(this long source, int sigValues = 0)
        {
            var seconds = source % 60;
            var minutes = (source / 60) % 60;
            var hours = (source / 3600) % 24;
            var days = (source / (24 * 3600));
            var returnValue = (days > 0 ? $"{days}d " : "");
            returnValue += (hours > 0 ? $"{hours}h " : "");
            returnValue += (minutes > 0 ? $"{minutes}m " : "");
            returnValue += (seconds > 0 ? $"{seconds}s" : "");

            if (sigValues > 0)
            {
                var tempArray = returnValue.Split(" ");
                returnValue = tempArray[0];
                if (sigValues > tempArray.Length) sigValues = tempArray.Length;
                for (int i = 1; i < sigValues; i++)
                    returnValue += $" {tempArray[i]}";
            }
            return returnValue;
        }

    }
}
