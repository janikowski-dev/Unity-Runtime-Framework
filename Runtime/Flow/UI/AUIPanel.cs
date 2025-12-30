using System;
using Internal.Runtime.Flow.States;
using LitMotion;
using UnityEngine;

namespace Internal.Runtime.Flow.UI
{
    // ReSharper disable once InconsistentNaming
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AUIPanel : MonoBehaviour
    {
        public event Action<Type> OnTransitionRequested;

        [SerializeField] private bool isInteractable;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            GetReferences();
            Disable(true);
        }

        public virtual MotionHandle Disable(bool shouldSkipAnimation = false)
        {
            ToggleInteraction(false);
            return LMotion.Create(_canvasGroup.alpha, 0f, shouldSkipAnimation ? 0f : 0.25f)
                .WithOnComplete(DisableCallback)
                .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                .Bind(x => _canvasGroup.alpha = x);
        }

        public virtual MotionHandle Enable()
        {
            ToggleInteraction(isInteractable);
            return LMotion.Create(_canvasGroup.alpha, 1f, 0.25f)
                .WithOnComplete(EnableCallback)
                .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                .Bind(x => _canvasGroup.alpha = x);
        }

        protected virtual void DisableCallback()
        {
        }
        
        protected virtual void EnableCallback()
        {
        }

        protected void RequestTransition<TState>() where TState : AState => OnTransitionRequested?.Invoke(typeof(TState));
        
        private void GetReferences() => _canvasGroup = GetComponent<CanvasGroup>();

        private void ToggleInteraction(bool state)
        {
            _canvasGroup.blocksRaycasts = state;
            _canvasGroup.interactable = state;
        }
    }
}