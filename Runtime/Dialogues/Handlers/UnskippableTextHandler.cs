using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Steps;
using TMPro;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class UnskippableTextHandler : ADialogueStepHandler<UnskippableTextStep>
    {
        private WaitForSeconds _nextLetterInterval;
        private TextMeshProUGUI _textContainer;
        private GameObject _continueContainer;
        private AudioSource _sfxPlayer;
        private AudioClip _letterSfx;
        private bool _allowSkip;
        
        public UnskippableTextHandler(DialogueReferences references, DialogueSettings settings)
        {
            _nextLetterInterval = new WaitForSeconds(settings.NextLetterInterval);
            _continueContainer = references.ContinueContainer;
            _textContainer = references.TextContainer;
            _allowSkip = settings.AllowTextSkip;
            _sfxPlayer = references.SfxPlayer;
            _letterSfx = settings.LetterSfx;
        }

        protected override void Skip(UnskippableTextStep step)
        {
            if (!_allowSkip)
            {
                return;
            }
            
            _textContainer.text = step.GetProcessedText();
            _continueContainer.SetActive(false);
        }

        protected override IEnumerator Handle(UnskippableTextStep step)
        {
            string text = step.GetProcessedText();
            
            if (!_allowSkip)
            {
                _continueContainer.SetActive(false);
            }
            
            step.CanBeSkipped = _allowSkip;
            step.IsFinished = false;
            int counter = 0;

            while (counter < text.Length - 1)
            {
                _textContainer.text = text[..++counter];
                _sfxPlayer.pitch = Random.Range(0.95f, 1.05f);
                _sfxPlayer.PlayOneShot(_letterSfx);
                yield return _nextLetterInterval;
            }

            _continueContainer.SetActive(true);
            _textContainer.text = text;
            step.IsFinished = true;
        }
    }
}