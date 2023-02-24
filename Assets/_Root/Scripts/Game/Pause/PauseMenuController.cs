using Tool;
using System;
using Profile;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

namespace Game
{
    internal class PauseMenuController : BaseController, IPauseHandler
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/PausePanel");
        private ProfilePlayer _profilePlayer;
        private PauseMenuModel _pauseModel;
        private PauseMenuView _view;


        public PauseMenuController([NotNull] Transform placeForUi, [NotNull] ProfilePlayer profilePlayer)
        {
            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _pauseModel 
                = profilePlayer.PauseModel ?? throw new ArgumentNullException(nameof(_pauseModel));
            _pauseModel.Register(this);

            _view = LoadView(placeForUi);
            _view.Init(ContinueGame, BackToMainMenu);
        }

        private PauseMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<PauseMenuView>();
        }

        private void ContinueGame()
        {
            _view.TurnOff();
            _pauseModel.SetPaused(false);
        }
        private void BackToMainMenu()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            _pauseModel.SetPaused(false);
            _view.TurnOff();
        }

        void IPauseHandler.SetPaused(bool isPaused)
        {
            if (isPaused) _view.TurnOn();
            else _view.TurnOff();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _pauseModel.UnRegister(this);
        }
    }
}
