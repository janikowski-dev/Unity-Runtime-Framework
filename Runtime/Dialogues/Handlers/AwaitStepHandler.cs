using System;
using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Proxies;
using Internal.Runtime.Dialogues.Steps;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class SimpleAwaitStepHandler : ADialogueStepHandler<AwaitStep>
    {
        private AwaitProxy _proxy;

        public SimpleAwaitStepHandler(DialogueReferences references)
        {
            _proxy = references.AwaitProxy;
        }

        protected override IEnumerator Handle(AwaitStep step)
        {
            Func<bool> finishCallback = _proxy.GetFinishCallback(step.Key);
            step.IsFinished = false;
            yield return new WaitUntil(() => finishCallback.Invoke());
            step.IsFinished = true;
        }
    }
}