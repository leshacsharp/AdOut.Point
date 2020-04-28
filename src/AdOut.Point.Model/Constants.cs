namespace AdOut.Point.Model
{
    public static class Constants
    {
        //_T - template
        public static class ValidationMessages
        {
            public const string TariffTimeLeavsOfBounds = "Tariff time({0}) leaves of AdPoint time working bounds({1})";
            public const string TariffTimeInteresection = "Your Tariff with time({0}) intersects another existing tariffs";
        }

        public static class HttpStatusCodes
        {
            public const int Status400BadRequest = 400;
            public const int Status401Unauthorized = 401;
            public const int Status403Forbidden = 403;
            public const int Status404NotFound = 404;
        }
    }
}
