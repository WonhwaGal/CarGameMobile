using Profile;
using Tool;
using UnityEngine;

namespace Game
{
    internal class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/GameUIView");
        private GameUIView _view;
        private PauseMenuModel _pauseModel;


        public GameUIController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _pauseModel = profilePlayer.PauseModel;
            _view = LoadView(placeForUi);

            _view.Init(OpenPauseMenu);
        }
        private GameUIView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameUIView>();
        }

        private void OpenPauseMenu()
        {
            _pauseModel.SetPaused(true);
        }
    }
}

