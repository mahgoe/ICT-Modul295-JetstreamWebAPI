namespace JetstreamSkiserviceAPI.Helpers
{
    public static class DateTimeHelper
    {
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }
    }
}
