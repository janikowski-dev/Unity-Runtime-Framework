using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Steps;
using TMPro;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class NameStepHandler : ADialogueStepHandler<NameStep>
    {
        private TextMeshProUGUI _nameContainer;
        
        public NameStepHandler(DialogueReferences references)
        {
            _nameContainer = references.NameContainer;
        }

        protected override IEnumerator Handle(NameStep step)
        {
            _nameContainer.text = step.Name;
            yield break;
        }
    }
}