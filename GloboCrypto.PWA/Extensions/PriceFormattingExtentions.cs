namespace GloboCrypto.PWA.Extensions
{
    public static class PriceFormattingExtentions
    {
        public static string FormatPrice(this string price)
        {
            var priceValue = double.Parse(price);
            return priceValue.ToString("#,##0.00");
        }
        public static string FormatPct(this string pricePct)
        {
            var priceValue = double.Parse(pricePct) * 100;
            return priceValue.ToString("#,##0.00");
        }
    }
}
