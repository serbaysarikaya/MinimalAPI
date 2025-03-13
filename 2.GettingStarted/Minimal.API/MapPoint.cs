namespace Minimal.API
{
    public class MapPoint
    {
        public double x { get; set; }
        public double y { get; set; }
        public static bool TryParse(string? value, out MapPoint? result)
        {
            try
            {
                var splitValue = value?.Split(",").Select(double.Parse).ToArray();
                result = new MapPoint()
                {
                    x = splitValue![0],
                    y = splitValue[1]
                };
                return true;
            }
            catch (Exception)
            {

                result = null;
                return false;
            }
        }
    }

}
