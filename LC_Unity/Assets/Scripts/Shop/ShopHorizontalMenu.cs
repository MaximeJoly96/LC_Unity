using Core;
using UI;

namespace Shop
{
    public class ShopHorizontalMenu : HorizontalMenu
    {
        protected override bool CanReceiveInput()
        {
            return GlobalStateMachine.Instance.CurrentState == GlobalStateMachine.State.InShopOptions;
        }
    }
}
