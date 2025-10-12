using System.Collections;
using Internal.Runtime.Dialogues.Steps;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class DelayStepHandler : ADialogueStepHandler<DelayStep>
    {
        protected override IEnumerator Handle(DelayStep step)
        {
            step.IsFinished = false;
            yield return new WaitForSeconds(step.DelayInSeconds);
            step.IsFinishedByDefaultInternal = true;
            step.IsFinished = true;
        }
    }
}