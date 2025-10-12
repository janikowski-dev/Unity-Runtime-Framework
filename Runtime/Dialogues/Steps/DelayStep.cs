namespace Internal.Runtime.Dialogues.Steps
{
    public class DelayStep : ADialogueStep
    {
        public override bool CanBeSkipped { get; set; } = false;
        public override bool IsFinishedByDefault
        {
            get => IsFinishedByDefaultInternal;
            set => IsFinishedByDefaultInternal = value;
        }

        public bool IsFinishedByDefaultInternal { get; set; } = false;
        public float DelayInSeconds { get; set; }
    }
}