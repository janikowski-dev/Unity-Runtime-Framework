using System;
using System.Collections;
using Internal.Runtime.Dialogues.Core;
using Internal.Runtime.Dialogues.Proxies;
using Internal.Runtime.Dialogues.Steps;
using Internal.Runtime.Dialogues.UI;

namespace Internal.Runtime.Dialogues.Handlers
{
    public class AnswerStepHandler : ADialogueStepHandler<AnswerStep>, IDownInputHandler, IUpInputHandler
    {
        private AAnswerHolder[] _answerHolders;
        private CallbackProxy _proxy;
        
        private Func<bool> _downInputCallback;
        private Func<bool> _upInputCallback;

        public AnswerStepHandler(DialogueReferences references)
        {
            _answerHolders = references.AnswerHolders;
            _proxy = references.CallbackProxy;
        }

        void IDownInputHandler.Init(Func<bool> downInputCallback)
        {
            _downInputCallback = downInputCallback;
        }

        void IUpInputHandler.Init(Func<bool> upInputCallback)
        {
            _upInputCallback = upInputCallback;
        }

        protected override void Skip(AnswerStep step)
        {
            MarkFinished(step);
            DisableHolders();
            ConfirmAnswer(step);
        }

        protected override IEnumerator Handle(AnswerStep step)
        {
            MarkUnfinished(step);
            InitAnswers(step);
            SelectAnswer(step, 0);
            yield return WaitForAnswer(step);
        }

        // ReSharper disable once IteratorNeverReturns
        private IEnumerator WaitForAnswer(AnswerStep step)
        {
            yield return null;

            while (true)
            {
                if (GotDownInput() && TryGetNextIndex(step, out int index))
                {
                    SelectAnswer(step, index);
                }

                if (GotUpInput() && TryGetPreviousIndex(step, out index))
                {
                    SelectAnswer(step, index);
                }

                yield return null;
            }
        }

        private bool GotUpInput() => _upInputCallback.Invoke();
        
        private bool GotDownInput() => _downInputCallback.Invoke();

        private bool TryGetNextIndex(AnswerStep step, out int index)
        {
            if (step.SelectedAnswerIndex < step.Answers.Length - 1)
            {
                index = step.SelectedAnswerIndex + 1;
                return true;
            }

            index = step.SelectedAnswerIndex;
            return false;
        }

        private void MarkFinished(AnswerStep step)
        {
            step.IsFinishedByDefaultInternal = true;
            step.IsFinished = true;
        }

        private void MarkUnfinished(AnswerStep step)
        {
            step.IsFinished = false;
        }

        private void ConfirmAnswer(AnswerStep step)
        {
            _proxy.RaiseCallbacks(step.Key, step.Answers[step.SelectedAnswerIndex]);
        }

        private bool TryGetPreviousIndex(AnswerStep step, out int index)
        {
            if (step.SelectedAnswerIndex > 0)
            {
                index = step.SelectedAnswerIndex - 1;
                return true;
            }

            index = step.SelectedAnswerIndex;
            return false;
        }

        private void InitAnswers(AnswerStep step)
        {
            for (int i = 0; i < _answerHolders.Length; i++)
            {
                _answerHolders[i].Toggle(i < step.Answers.Length);
            }

            for (int i = 0; i < step.Answers.Length; i++)
            {
                _answerHolders[i].Init(step.Answers[i]);
            }
        }

        private void SelectAnswer(AnswerStep step, int index)
        {
            for (int i = 0; i < _answerHolders.Length; i++)
            {
                if (i == index)
                {
                    _answerHolders[i].Select();
                }
                else
                {
                    _answerHolders[i].Unselect();
                }
            }
            
            step.SelectedAnswerIndex = index;
        }

        private void DisableHolders()
        {
            foreach (AAnswerHolder answerHolder in _answerHolders)
            {
                answerHolder.Toggle(false);
            }
        }
    }
}