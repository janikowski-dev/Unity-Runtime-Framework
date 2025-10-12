namespace Internal.Runtime.Dialogues.Steps
{
    public class AwaitStep : ADialogueStep
    {
        public override bool IsFinishedByDefault => false;
        
        public string Key { get; set; }
    }
}