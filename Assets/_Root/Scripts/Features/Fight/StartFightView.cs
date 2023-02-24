using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Game;
using System;

namespace Features.Fight
{
    internal class StartFightView : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private Button _startFightButton;

        private PauseMenuModel _pauseModel;
        private bool IsPaused;


        public void Init(UnityAction startFight, PauseMenuModel pauseModel)
        {
            _pauseModel
                = pauseModel ?? throw new ArgumentNullException(nameof(pauseModel));
            _pauseModel.Register(this);


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
            _pauseModel.UnRegister(this);
        }
    }
}
