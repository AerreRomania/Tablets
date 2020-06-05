using System;

namespace SmartB.Core.Extensions
{
    public static class SettingExtensions
    {
        public static int ToInteger(this string hour)
        {
            return !string.IsNullOrEmpty(hour) ? Convert.ToInt32(hour) : 0;
        }
        public static double ToDouble(this string hour)
        {
            return !string.IsNullOrEmpty(hour) ? Convert.ToDouble(hour) : 0;
        }

    }
}
