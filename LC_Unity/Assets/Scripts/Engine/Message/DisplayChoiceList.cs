using System.Collections.Generic;
using Engine.Events;

namespace Engine.Message
{
    public struct Choice
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class DisplayChoiceList : IRunnable
    {
        public List<Choice> Choices { get; set; }

        public void Add(Choice choice)
        {
            if (Choices == null)
                Choices = new List<Choice>();

            Choices.Add(choice);
        }

        public void Run()
        {

        }
    }
}
