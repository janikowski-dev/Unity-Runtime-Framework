using System;
using System.Collections.Generic;
using System.Linq;
using Internal.Runtime.Flow.States;
using UnityEngine;

namespace Internal.Runtime.Flow.UI
{
    // ReSharper disable once InconsistentNaming
    public abstract class AUIPanelsProvider : MonoBehaviour
    {
        [field: SerializeField] public GameObject PanelsParent { get; set; }
        
        private Dictionary<Type, Type> _panelTypeByStateTypes = new();
        
        public Dictionary<Type, AUIPanel> PanelsByStateType { get; } = new();

        private void Awake()
        {
            DontDestroyOnLoad();
            AddTranslations();
            GetPanels();
        }

        protected abstract void AddTranslations();
        
        protected void AddTranslation<TState, TPanel>() where TState : AState where TPanel : AUIPanel =>
            _panelTypeByStateTypes.Add(typeof(TState), typeof(TPanel));

        private void DontDestroyOnLoad() => DontDestroyOnLoad(PanelsParent);
        
        private void GetPanels()
        {
            AUIPanel[] panels = PanelsParent.GetComponentsInChildren<AUIPanel>();
            
            foreach ((Type stateType, Type panelType) in _panelTypeByStateTypes)
            {
                PanelsByStateType.Add(stateType, panels.First(panel => panelType == panel.GetType()));
            }
        }
    }
}