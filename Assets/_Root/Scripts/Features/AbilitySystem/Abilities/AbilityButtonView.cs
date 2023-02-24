using Game;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Features.AbilitySystem.Abilities
{
    internal interface IAbilityButtonView
    {
        void Init(Sprite icon, UnityAction click);
        void Deinit();
    }

    internal class AbilityButtonView : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;

        private PauseMenuModel _pauseModel;

        private bool IsPaused;

        private void OnDestroy() => Deinit();


        public void Init(Sprite icon, UnityAction click, PauseMenuModel pauseModel)
        {
            _icon.sprite = icon;

            _pauseModel
                = pauseModel ?? throw new ArgumentNullException(nameof(pauseModel));
            _pauseModel.Register(this);

            _button.onClick.AddListener(() =>
            {
                if (IsPaused)
                    return;

                click();
            });
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
        }

        public void Deinit()
        {
            _icon.sprite = null;
            _button.onClick.RemoveAllListeners();
            _pauseModel.UnRegister(this);
        }
    }
}
