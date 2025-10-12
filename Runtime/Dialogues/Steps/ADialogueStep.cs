namespace Internal.Runtime.Dialogues.Steps
{
    public abstract class ADialogueStep
    {
        public virtual bool IsFinishedByDefault { get; set; } = true;
        public virtual bool CanBeSkipped { get; set; } = true;
        public virtual bool IsFinished { get; set; } = true;
    }
}