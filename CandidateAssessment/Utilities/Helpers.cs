﻿using System;
using static CandidateAssessment.Utilities.Constants;

namespace CandidateAssessment.Utilities
{
    public static class Helpers
    {
        /* Convert the provided number to words: https://stackoverflow.com/a/1691675/522859 */
        public static string NumberToWords(long input)
        {
            if (input < 0) throw new ArgumentException($"Input must be greater than or equal to zero: ${input}");
            if (input > ChequeConfiguration.MaximumChequeAmount) throw new ArgumentException($"Input must be less than or equal to ${ChequeConfiguration.MaximumChequeAmount}: ${input}");

            if (input < 20) return NumberWords.ZeroToNineteen[input];
            if (input < 100) return $"{NumberWords.Tens[((int)input / 10) - 1]} {NumberToWords(input % 10)}";
            //if (input < 999) return $"{NumberWords.ZeroToNineteen[input / 100 - 1]}"
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
