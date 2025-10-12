using Internal.Runtime.Utilities;

namespace Internal.Runtime.Dialogues.Steps
{
    public class UnskippableTextStep : ADialogueStep
    {
        public override bool IsFinishedByDefault => false;

        public string Text { get; set; }

        public string GetProcessedText() => VariablesProcessor.GetProcessedText(Text);
    }
}