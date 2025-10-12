using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Runtime.Dependencies.Core;
using Internal.Runtime.Flow.States;
using Internal.Runtime.Flow.UI;
using Internal.Runtime.Utilities;
using UnityEngine;

namespace Internal.Runtime.Flow
{
    [DefaultExecutionOrder(-500)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject uiParent;
        
        private AUIPanelsProvider _panelsProvider;
        private AStatesProvider _statesProvider;
        private AUIPanel _currentPanel;
        private AState _currentState;

        private void Awake()
        {
            GetReferences();
            InjectSceneDependencies();
            InjectStateDependencies();
            InjectUIDependencies();
        }

        private void Start()
        {
            DontDestroyOnLoad();
            EnterInitialState();
            InitUITransitions();
        }

        private void Update()
        {
            if (!TryGetNextStateType(out Type stateType))
            {
                return;
            }

            TogglePanel(stateType);
            EnterState(stateType);
        }

        private void InjectSceneDependencies() => DependencyInjector.Inject(GetSceneDependencies());

        private void InjectStateDependencies() => DependencyInjector.Inject(GetStateDependencies());

        private void InjectUIDependencies() => DependencyInjector.Inject(GetUIDependencies());

        private void DontDestroyOnLoad() => DontDestroyOnLoad(gameObject);

        private void EnterInitialState()
        {
            TogglePanel(GetInitialState());
            EnterState(GetInitialState());
        }

        private void InitUITransitions()
        {
            foreach (AUIPanel panel in uiParent.GetComponentsInChildren<AUIPanel>())
            {
                panel.OnTransitionRequested += TogglePanel;
                panel.OnTransitionRequested += EnterState;
            }
        }

        private IDependency[] GetSceneDependencies() => GetComponentsInChildren<IDependency>();

        private IDependency[] GetStateDependencies()
        {
            IEnumerable<IDependency> stateDependencies = _statesProvider.StatesByType.Values.OfType<IDependency>();
            return stateDependencies.ToArray();
        }

        private IDependency[] GetUIDependencies()
        {
            IEnumerable<IDependency> uiDependencies = _panelsProvider.PanelsParent.GetComponentsInChildren<IDependency>();
            return uiDependencies.ToArray();
        }

        private Type GetInitialState() => _statesProvider.InitialStateType;

        private bool TryGetNextStateType(out Type stateType)
        {
            StateTransition transition = _currentState?.Transitions.FirstOrDefault(transition => transition.Conditions.All(condition => condition.Invoke()));
            stateType = transition?.TargetType;

            if (stateType != null)
            {
                return true;
            }
            
            stateType = _currentState?.OnUpdate();
            return stateType != null;
        }

        private void TogglePanel(Type stateType)
        {
            AUIPanel newPanel = _panelsProvider.PanelsByStateType.GetValueOrDefault(stateType);
            AUIPanel oldPanel = _currentPanel;
            _currentPanel = newPanel;

            if (oldPanel == newPanel)
            {
                return;
            }

            if (oldPanel != null)
            {
                oldPanel.Disable();
            }

            if (newPanel != null)
            {
                newPanel.Enable();
            }
            
            ExtendedDebug.Log($"Changing panel [{oldPanel}] to [{newPanel}]");
        }

        private void EnterState(Type stateType)
        {
            AState newState = _statesProvider.StatesByType[stateType];
            AState oldState = _currentState;
            _currentState = newState;
            
            oldState?.OnExit();
            newState?.OnEnter();

            ExtendedDebug.Log($"Changing state [{oldState}] to [{newState}]");
        }

        private void GetReferences()
        {
            _panelsProvider = GetComponent<AUIPanelsProvider>();
            _statesProvider = GetComponent<AStatesProvider>();
        }
    }
}