using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Runtime.Dialogues.Handlers;
using Internal.Runtime.Dialogues.Steps;
using Internal.Runtime.Utilities;
using UnityEngine;

namespace Internal.Runtime.Dialogues.Core
{
    public class Dialogue
    {
        public List<ADialogueStep> Steps { get; } = new();
    }
    
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] private DialogueReferences references;
        [SerializeField] private DialogueSettings settings;

        private Dictionary<Type, IDialogueStepHandler> _handlerByStepTypes = new();
        
        private Func<bool> _continueInputCallback;
        private Func<bool> _downInputCallback;
        private Func<bool> _upInputCallback;
        private GameObjectProxy _proxy;
        private Dialogue _dialogue;
        private int _stepIndex;
        private bool _isInit;

        public void Init(Func<bool> continueInputCallback, Func<bool> upInputCallback, Func<bool> downInputCallback)
        {
            Cache(continueInputCallback, upInputCallback, downInputCallback);
            InjectInput();
        }

        public void Init(Dialogue dialogue)
        {
            Init();
            MarkInit();
            ResetSteps();
            Cache(dialogue);
            StartStep();
        }

        public void Reinit(DialogueReferences newReferences, DialogueSettings newSettings)
        {
            Override(newReferences, newSettings);
            ClearHandlers();
            GetHandlers();
        }
        
        public bool Tick()
        {
            if (ShouldAutocompleteStep())
            {
                NextStep();
            
                if (IsDialogueOver() || TryPrefinishingSteps())
                {
                    return true;
                }
                
                ExtendedDebug.Log($"Automatically going to the next step from: {GetStepInfo()}");
                StartStep();
                ExtendedDebug.Log($"Automatically starting step: {GetStepInfo()}");
                return false;
            }
            
            if (!GotContinueInput())
            {
                return false;
            }
            
            ExtendedDebug.Log("Got continue input");
            
            if (IsDialogueOver() || TryPrefinishingSteps())
            {
                return true;
            }
                
            if (CanBeSkipped())
            {
                ExtendedDebug.Log($"Skipping step: {GetStepInfo()}");
                SkipStep();
                return false;
            }

            if (!IsFinished())
            {
                return false;
            }
            
            NextStep();

            if (IsDialogueOver() || TryPrefinishingSteps())
            {
                return true;
            }

            ExtendedDebug.Log($"Going to the next step from: {GetStepInfo()}");
            StartStep();
            ExtendedDebug.Log($"Starting step: {GetStepInfo()}");
            return false;
        }

        private void Init()
        {
            if (!ShouldInit())
            {
                return;
            }
            
            ClearHandlers();
            GetHandlers();
            GetReferences();
        }

        private string GetStepInfo() => _dialogue.Steps[_stepIndex].GetType().ToString();

        private bool ShouldAutocompleteStep() => _stepIndex < _dialogue.Steps.Count && _dialogue.Steps[_stepIndex].IsFinishedByDefault && _dialogue.Steps[_stepIndex].IsFinished;
        
        private bool CanBeSkipped() => !_dialogue.Steps[_stepIndex].IsFinished && _dialogue.Steps[_stepIndex].CanBeSkipped;

        private bool IsFinished() => _dialogue.Steps[_stepIndex].IsFinished;

        private bool IsDialogueOver() => _stepIndex >= _dialogue.Steps.Count;

        private bool GotContinueInput() => _continueInputCallback.Invoke();
        
        private void SkipStep()
        {
            _proxy.StopAllCoroutines();
            _handlerByStepTypes[_dialogue.Steps[_stepIndex].GetType()].Skip(_dialogue.Steps[_stepIndex]);
        }
        
        private void NextStep() => _stepIndex++;

        private void StartStep()
        {
            _proxy.StopAllCoroutines();
            _proxy.StartCoroutine(_handlerByStepTypes[_dialogue.Steps[_stepIndex].GetType()].Handle(_dialogue.Steps[_stepIndex]));
        }

        private bool TryPrefinishingSteps()
        {
            while (_dialogue.Steps[_stepIndex].IsFinishedByDefault)
            {
                _proxy.StartCoroutine(_handlerByStepTypes[_dialogue.Steps[_stepIndex].GetType()].Handle(_dialogue.Steps[_stepIndex]));

                if (++_stepIndex >= _dialogue.Steps.Count)
                {
                    return true;
                }
            }

            return false;
        }

        private bool ShouldInit() => !_isInit;

        private void MarkInit() => _isInit = true;

        private void ResetSteps() => _stepIndex = 0;
        
        private void Cache(Func<bool> continueInputCallback, Func<bool> upInputCallback, Func<bool> downInputCallback)
        {
            _continueInputCallback = continueInputCallback;
            _downInputCallback = downInputCallback;
            _upInputCallback = upInputCallback;
        }

        private void Cache(Dialogue dialogue) => _dialogue = dialogue;

        private void InjectInput()
        {
            foreach (IDownInputHandler downHandler in _handlerByStepTypes.Values.OfType<IDownInputHandler>())
            {
                downHandler.Init(_downInputCallback);
            }
            
            foreach (IUpInputHandler upHandler in _handlerByStepTypes.Values.OfType<IUpInputHandler>())
            {
                upHandler.Init(_upInputCallback);
            }
        }

        private void GetHandlers()
        {
            _handlerByStepTypes.Add(typeof(UnskippableTextStep), new UnskippableTextHandler(references, settings));
            _handlerByStepTypes.Add(typeof(TextStep), new TextStepHandler(references, settings));
            _handlerByStepTypes.Add(typeof(IconStep), new IconStepHandler(references));
            _handlerByStepTypes.Add(typeof(ClearStep), new ClearStepHandler(references));
            _handlerByStepTypes.Add(typeof(NameStep), new NameStepHandler(references));
            _handlerByStepTypes.Add(typeof(AnswerStep), new AnswerStepHandler(references));
            _handlerByStepTypes.Add(typeof(CallbackStep), new CallbackStepHandler(references));
            _handlerByStepTypes.Add(typeof(AwaitStep), new SimpleAwaitStepHandler(references));
            _handlerByStepTypes.Add(typeof(DelayStep), new DelayStepHandler());
        }

        private void Override(DialogueReferences newReferences, DialogueSettings newSettings)
        {
            references = newReferences;
            settings = newSettings;
        }

        private void ClearHandlers()
        {
            _handlerByStepTypes.Clear();
        }

        private void GetReferences()
        {
            _proxy = GameObjectProxy.Get<DialogueController>();
        }
    }
}