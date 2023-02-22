using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Tool.Pause;

namespace Features.Fight
{
    internal class StartFightView : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Button _startFightButton;

        private bool IsPaused;
        public void Init(UnityAction startFight)
        {
            PauseController.Instance.Register(this);
            _startFightButton.onClick.AddListener( () =>
            {
                if (IsPaused)
                    return;
                startFight();
            });
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
        }

        private void OnDestroy()
        {
            _startFightButton.onClick.RemoveAllListeners();
            PauseController.Instance.Register(this);
        }
    }
}
