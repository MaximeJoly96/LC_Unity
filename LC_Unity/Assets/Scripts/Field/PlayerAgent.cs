namespace Field
{
    public class PlayerAgent : Agent
    {
        private void Awake()
        {
            AgentsManager.Instance.RegisterAgent(this);
        }
    }
}
