using Ui;
using Game;
using Profile;
using UnityEngine;
using Features.Shed;


    internal class MainController : BaseController
    {
        private readonly Transform _placeForUi;
        private readonly ProfilePlayer _profilePlayer;

        private MainMenuController _mainMenuController;
        private GameController _gameController;
        private SettingsContrroller _settingsController;

        private ShedContext _shedContext;

        public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _placeForUi = placeForUi;
            _profilePlayer = profilePlayer;

            profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
            OnChangeGameState(_profilePlayer.CurrentState.Value);
        }

        protected override void OnDispose()
        {
            DisposeControllers();

            _profilePlayer.CurrentState.UnSubscribeOnChange(OnChangeGameState);
        }

        private void OnChangeGameState(GameState state)
        {
            DisposeControllers();

            switch (state)
            {
                case GameState.Start:
                    _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                    break;
                case GameState.Game:
                    _gameController = new GameController(_placeForUi, _profilePlayer);
                    break;
                case GameState.Settings:
                    _settingsController = new SettingsContrroller(_placeForUi, _profilePlayer);
                    break;
                case GameState.Shed:
                   _shedContext = new ShedContext(_placeForUi, _profilePlayer);
                   break;
        }
        }
        private void DisposeControllers()
        {
            _mainMenuController?.Dispose();
            _gameController?.Dispose();
            _settingsController?.Dispose();

            _shedContext?.Dispose();
        }
    }

