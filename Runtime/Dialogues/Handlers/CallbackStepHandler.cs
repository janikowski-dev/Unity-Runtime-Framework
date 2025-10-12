using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Proxies;
using Internal.Runtime.Dialogues.Steps;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class CallbackStepHandler : ADialogueStepHandler<CallbackStep>
    {
        private CallbackProxy _proxy;
        
        public CallbackStepHandler(DialogueReferences references)
        {
            _proxy = references.CallbackProxy;
        }
        
        protected override IEnumerator Handle(CallbackStep step)
        {
            _proxy.RaiseCallbacks(step.Key, string.Empty);
            yield break;
        }
    }
}