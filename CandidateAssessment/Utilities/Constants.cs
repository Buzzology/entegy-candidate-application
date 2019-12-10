namespace CandidateAssessment.Utilities
{
    public static class Constants
    {
        public static class ChequeConfiguration
        {
            public const double MaximumChequeAmount = 999999999;
            public const double MinimumChequeAmount = 0.01;
        }

        public static class MoneyWords
        {
            public const string Dollar = "dollar";
            public const string Dollars = "dollars";
            public const string Cent = "cent";
            public const string Cents = "cents";
            public const string Only = "only";
        }

        public static class NumberWords
        {
            public const string Hundred = "hundred";
            public const string Thousand = "thousand";
            public const string Million = "million";
            public static readonly string[] ZeroToNineteen = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            public static readonly string[] Tens = { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        }
    }
}
