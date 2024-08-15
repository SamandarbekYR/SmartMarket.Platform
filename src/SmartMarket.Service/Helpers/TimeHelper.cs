namespace SmartMarket.Service.Helpers
{
    public class TimeHelper
    {
        public static DateTime GetDateTime()
        {
            var time = DateTime.UtcNow;
            time = time.AddHours(5);

            return time;
        }
    }
}
