namespace PersonalFinanceApp.Helpers
{
    public class PhoneHelpers
    {
        public static bool TryParseAsPhone(string value, out string phone)
        {
            var digits = new string(value.Where(p => char.IsDigit(p)).ToArray());
            var length = digits.Length;

            if (length < 10 || length > 11 || (length == 11 && !digits.StartsWith("7") && !digits.StartsWith("8")))
            {
                phone = null;
                return false;
            }

            phone = FormatPhone(digits);
            return true;
        }

        public static string FormatPhone(string digits)
        {
            if (digits.Length == 11 && digits.StartsWith("8"))
            {
                digits = digits.Remove(0, 1);
            }

            if (digits.Length == 10)
            {
                digits = "7" + digits;
            }
            return digits;
        }
    }
}
