using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Steps;

namespace Internal.Runtime.Dialogues.Handlers
{
    public abstract class ADialogueStepHandler<TStep> : IDialogueStepHandler where TStep : ADialogueStep
    {
        public IEnumerator Handle(ADialogueStep step) => Handle((TStep)step);

        public void Skip(ADialogueStep step) => Skip((TStep)step);

        protected abstract IEnumerator Handle(TStep step);
        
        protected virtual void Skip(TStep step)
        {
        }
    }
}