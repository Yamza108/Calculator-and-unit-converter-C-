using System.Globalization;

namespace CalcConverter
{
    // Turns text typed by the user into a number, and tells us if that failed.
    public static class InputParser
    {
        // TryParseNumber returns true if "text" is a valid number, false if not.
        // The number itself comes back through the "out" parameter.
        // (An "out" parameter is a second way for a method to give something back.)
        public static bool TryParseNumber(string text, out decimal number)
        {
            // InvariantCulture means "always use a dot as the decimal point",
            // so 12.5 works on every computer.
            return decimal.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out number);
        }
    }
}
