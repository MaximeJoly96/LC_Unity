namespace Utils
{
    public static class MeasuresConverter
    {
        public static float WorldUnitsToRange(float distanceWorldUnits)
        {
            return distanceWorldUnits * 500.0f;
        }

        public static float RangeToWorldUnits(float range)
        {
            return range / 500.0f;
        }
    }
}
