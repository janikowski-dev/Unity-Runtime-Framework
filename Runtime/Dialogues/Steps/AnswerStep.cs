namespace Internal.Runtime.Dialogues.Steps
{
    public class AnswerStep : CallbackStep
    {
        public bool IsFinishedByDefaultInternal { get; set; } = false;

        public override bool IsFinishedByDefault
        {
            get => IsFinishedByDefaultInternal;
            set => IsFinishedByDefaultInternal = value;
        }
        
        public int SelectedAnswerIndex { get; set; }
        public string[] Answers { get; set; }
    }
}