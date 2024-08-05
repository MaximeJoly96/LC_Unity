using UnityEngine;

namespace Utils
{
    public static class TimeConverter
    {
        public static string FormatTimeFromSeconds(float seconds)
        {
            int floor = Mathf.FloorToInt(seconds);

            int secondsInt = floor % 60;
            int hoursInt = floor / 3600;
            int minutesInt = (floor / 60) % 60;

            return hoursInt.ToString("00") + ":" + minutesInt.ToString("00") + ":" + secondsInt.ToString("00");
        }
    }
}
