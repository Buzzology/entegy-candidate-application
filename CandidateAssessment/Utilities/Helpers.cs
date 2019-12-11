using System;
using static CandidateAssessment.Utilities.Constants;

namespace CandidateAssessment.Utilities
{
    public static class Helpers
    {
        /* Convert the provided number to words: https://stackoverflow.com/a/1691675/522859 */
        public static string NumberToWords(long input)
        {
            // Verify valid arguments
            if (input < 0) throw new ArgumentException($"Input must be greater than or equal to zero: ${input}");
            if (input > ChequeConfiguration.MaximumChequeAmount) throw new ArgumentException($"Input must be less than or equal to ${ChequeConfiguration.MaximumChequeAmount}: ${input}");

            // Recursively convert numbers to words
            if (input == 0) return "";
            if (input < 20) return NumberWords.ZeroToNineteen[input];
            if (input < 100) return (NumberWords.Tens[((int)input / 10) - 1] + (input % 10 > 0 ? "-" + NumberToWords(input % 10) : string.Empty)).Trim();
            if (input < 999) return $"{NumberWords.ZeroToNineteen[input / 100]} {NumberWords.Hundred} {NumberToWords(input % 100)}".Trim();
            if (input < 999999) return $"{NumberToWords((int) input / 1000)} {NumberWords.Thousand} {NumberToWords(input % 1000)}".Trim();
            if (input < 999999999) return $"{NumberToWords((int) input / 1000000)} {NumberWords.Million} {NumberToWords(input % 1000000)}".Trim();
            else throw new ArgumentOutOfRangeException("input", "Number to words input must be less than 1 billion.");
        }


        /* Round a decimal down to the specified number of decimal places: https://stackoverflow.com/a/13522109/522859 */
        public static decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }


        /* Extract decimal portion of number as an int: https://stackoverflow.com/a/13038524/522859 */
        public static int GetDecimalPortionAsInt(decimal input)
        {
            return (int)(input % 1 * 100);
        }


        /* TODO: (CJO) Log error to logging service (i.e. raygun) */
        public static void LogError(Exception e, string description)
        {
            
        }
    }
}
