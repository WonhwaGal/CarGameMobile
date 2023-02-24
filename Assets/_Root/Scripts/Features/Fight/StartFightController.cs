using Game;
using Profile;
using System;
using Tool;
using UnityEngine;

namespace Features.Fight
{
    internal class StartFightController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Fight/StartFightView");

        private readonly StartFightView _view;
        private readonly ProfilePlayer _profilePlayer;
        private PauseMenuModel _pauseModel;

        public StartFightController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;

            _pauseModel
                = profilePlayer.PauseModel ?? throw new ArgumentNullException(nameof(profilePlayer.PauseModel));

            _view = LoadView(placeForUi);
            _view.Init(StartFight, _pauseModel);
        }


        private StartFightView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<StartFightView>();
        }

        private void StartFight() =>
            _profilePlayer.CurrentState.Value = GameState.Fight;
    }
}
