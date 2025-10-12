using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Proxies;
using Internal.Runtime.Dialogues.Steps;
using UnityEngine.UI;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class IconStepHandler : ADialogueStepHandler<IconStep>
    {
        private Image _iconContainer;
        private IconProxy _proxy;
        
        public IconStepHandler(DialogueReferences references)
        {
            _iconContainer = references.IconContainer;
            _proxy = references.IconProxy;
        }

        protected override IEnumerator Handle(IconStep step)
        {
            _iconContainer.sprite = _proxy.GetIcon(step.Key);
            yield break;
        }
    }
}