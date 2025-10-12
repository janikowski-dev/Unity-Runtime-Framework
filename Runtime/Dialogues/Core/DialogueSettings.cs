using System;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Core
{
    [Serializable]
    public class DialogueSettings
    {
        [field: SerializeField] public float NextLetterInterval { get; set; }
        [field: SerializeField] public bool AllowTextSkip { get; set; }
        [field: SerializeField] public AudioClip LetterSfx { get; set; }
    }
}