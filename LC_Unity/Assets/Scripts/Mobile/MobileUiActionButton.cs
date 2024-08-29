using Movement;

namespace Mobile
{
    public class MobileUiActionButton : MobileUiButton
    {
        public override void Execute()
        {
            FindObjectOfType<PlayerController>().CheckForInteraction();
        }
    }
}
