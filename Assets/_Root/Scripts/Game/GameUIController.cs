using Tool;
using Profile;
using UnityEngine;

namespace Game
{
    internal class GameUIController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Game/GameUIView");
        private readonly ProfilePlayer _profilePlayer;
        private readonly GameUIView _view;

        public GameUIController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init();

            Subscribe(_view);
        }

        private GameUIView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<GameUIView>();
        }

        protected override void OnDispose() => Unsubscribe(_view);

        private void Subscribe(GameUIView view)
        {
            view.ExitButton.onClick.AddListener(ReturnToMenu);

            view.BackToMenuButton.onClick.AddListener(ReturnToMenu);
        }

        private void Unsubscribe(GameUIView view)
        {
            view.ExitButton.onClick.RemoveListener(ReturnToMenu);
        }

        private void ReturnToMenu() => _profilePlayer.CurrentState.Value = GameState.Start;
    }
}

