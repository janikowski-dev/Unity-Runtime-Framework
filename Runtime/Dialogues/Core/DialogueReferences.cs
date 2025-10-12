using System;
using Internal.Runtime.Dialogues.Proxies;
using Internal.Runtime.Dialogues.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Runtime.Dialogues.Core
{
    [Serializable]
    public class DialogueReferences
    {
        [field: SerializeField] public TextMeshProUGUI TextContainer { get; set; }
        [field: SerializeField] public TextMeshProUGUI NameContainer { get; set; }
        [field: SerializeField] public AAnswerHolder[] AnswerHolders { get; set; }
        [field: SerializeField] public GameObject ContinueContainer { get; set; }
        [field: SerializeField] public Image IconContainer { get; set; }
        [field: SerializeField] public AudioSource SfxPlayer { get; set; }
        [field: SerializeField] public AwaitProxy AwaitProxy { get; set; }
        [field: SerializeField] public CallbackProxy CallbackProxy { get; set; }
        [field: SerializeField] public IconProxy IconProxy { get; set; }
    }
}