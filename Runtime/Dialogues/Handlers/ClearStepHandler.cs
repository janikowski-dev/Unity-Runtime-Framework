using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Steps;
using TMPro;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class ClearStepHandler : ADialogueStepHandler<ClearStep>
    {
        private TextMeshProUGUI _textContainer;

        public ClearStepHandler(DialogueReferences references)
        {
            _textContainer = references.TextContainer;
        }
        
        protected override IEnumerator Handle(ClearStep step)
        {
            _textContainer.text = string.Empty;
            yield break;
        }
    }
}