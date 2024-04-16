namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            string digit = Convert.ToString(count);
            int lastDigit = 0;
            if (digit.Length > 1)
            {
                lastDigit = Convert.ToInt32(digit.Substring(digit.Length - 2));
            }
            // Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.
            if (count % 10 == 1 && lastDigit != 11)
                return "рубль";
            else if ((count % 10 == 2 || count % 10 == 3 || count % 10 == 4)
                && lastDigit != 12 && lastDigit != 13 && lastDigit != 14)
                return "рубля";
            else
                return "рублей";
        }
    }
}