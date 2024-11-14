using System.Globalization;

namespace UI.UIExtensions
{
    public static class MoneyStringIFormatter
    {
        public static string FormatToMoney(this int money)
        {
            var customTextFormatter = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            customTextFormatter.NumberGroupSeparator = " ";
            return $"$ {money.ToString("#,0", customTextFormatter)}";
        }
        
        public static string FormatToMoney(this float money)
        {
            var customTextFormatter = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            customTextFormatter.NumberGroupSeparator = " ";
            return $"$ {money.ToString("#,0", customTextFormatter)}";
        }
    }
}