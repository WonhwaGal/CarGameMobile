using Game;
using System;
using UnityEngine;
using System.Collections.Generic;
using Features.AbilitySystem.Abilities;


namespace Features.AbilitySystem
{
    internal interface IAbilitiesView
    {
        void Display(IEnumerable<IAbilityItem> abilityItems, Action<string> clicked, PauseMenuModel pauseModel);
        void Clear();
    }

    internal class AbilitiesView : MonoBehaviour, IAbilitiesView
    {
        [SerializeField] private GameObject _abilityButtonPrefab;
        [SerializeField] private Transform _placeForButtons;

        private readonly Dictionary<string, AbilityButtonView> _buttonViews = new Dictionary<string, AbilityButtonView>();


        private void OnDestroy() => Clear();


        public void Display(IEnumerable<IAbilityItem> abilityItems, Action<string> clicked, PauseMenuModel pauseModel)
        {
            Clear();

            foreach (IAbilityItem abilityItem in abilityItems)
                _buttonViews[abilityItem.Id] = CreateButtonView(abilityItem, clicked, pauseModel);
        }

        public void Clear()
        {
            foreach (AbilityButtonView buttonView in _buttonViews.Values)
                DestroyButtonView(buttonView);

            _buttonViews.Clear();
        }


        private AbilityButtonView CreateButtonView(IAbilityItem item, Action<string> clicked, PauseMenuModel pauseModel)
        {
            GameObject objectView = Instantiate(_abilityButtonPrefab, _placeForButtons, false);
            AbilityButtonView buttonView = objectView.GetComponent<AbilityButtonView>();

            buttonView.Init
            (
                item.Icon,
                () => clicked?.Invoke(item.Id),
                pauseModel
            );

            return buttonView;
        }

        private void DestroyButtonView(AbilityButtonView buttonView)
        {
            buttonView.Deinit();
            Destroy(buttonView.gameObject);
        }
    }
}
