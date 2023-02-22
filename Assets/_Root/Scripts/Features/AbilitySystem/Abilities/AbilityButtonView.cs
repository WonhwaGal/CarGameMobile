using Tool.Pause;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

        private bool IsPaused;

        private void OnDestroy() => Deinit();


        public void Init(Sprite icon, UnityAction click)
        {
            _icon.sprite = icon;
            PauseController.Instance.Register(this);
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
            PauseController.Instance.UnRegister(this);
        }
    }
}
