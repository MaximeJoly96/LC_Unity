using Engine.SystemSettings;

namespace Field
{
    public class AutoplayChangeToField : RunnableAgent
    {
        public override void RunSequence()
        {
            ChangeGameState change = new ChangeGameState { State = "OnField" };

            change.Run();

            Runner.Finished.Invoke();
        }

        protected override void OnEnable()
        {
            RunSequence();
        }
    }
}
