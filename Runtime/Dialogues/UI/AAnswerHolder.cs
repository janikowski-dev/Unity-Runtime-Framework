using TMPro;
using UnityEngine;

namespace Internal.Runtime.Dialogues.UI
{
    public abstract class AAnswerHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void Toggle(bool state)
        {
            gameObject.SetActive(state);
        }

        public void Init(string answer)
        {
            text.text = answer;
        }
        
        public abstract void Select();
        
        public abstract void Unselect();
    }
}