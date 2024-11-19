namespace Save.Model
{
    public class SaveDescriptor
    {
        public int Id { get; private set; }
        public int MapId { get; private set; }
        public float InGameTime { get; private set; }

        public SaveDescriptor(int id, int mapId, float inGameTime)
        {
            Id = id;
            MapId = mapId;
            InGameTime = inGameTime;
        }
    }
}
