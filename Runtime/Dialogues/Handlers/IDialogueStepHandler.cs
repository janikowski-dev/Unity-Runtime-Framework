using System.Collections;
using Internal.Runtime.Dialogues.Steps;

namespace Internal.Runtime.Dialogues.Handlers
{
    public interface IDialogueStepHandler
    {
        IEnumerator Handle(ADialogueStep step);
        void Skip(ADialogueStep step);
    }
}