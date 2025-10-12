using Internal.Runtime.Dialogues.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Runtime.Dialogues.UI
{
    public class DefaultAnswerHolder : AAnswerHolder
    {
        [SerializeField] private Image background;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;

        public override void Select()
        {
            background.color = selectedColor;
        }

        public override void Unselect()
        {
            background.color = unselectedColor;
        }
    }
}