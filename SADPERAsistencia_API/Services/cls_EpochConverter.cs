namespace SADPERAsistencia_API.Services
{
    public class cls_EpochConverter
    {
        public long DateTimeToEpoch(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)timeSpan.TotalSeconds;
        }

        public DateTime EpochToDateTime(long epochTime)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epochTime);
        }
    }
}
