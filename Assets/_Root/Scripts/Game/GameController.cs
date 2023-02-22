using Tool;
using Profile;
using UnityEngine;
using Game.Car;
using Game.InputLogic;
using Game.TapeBackground;
using Features.AbilitySystem;

namespace Game
{
    internal class GameController : BaseController
    {
        private readonly SubscriptionProperty<float> _leftMoveDiff;
        private readonly SubscriptionProperty<float> _rightMoveDiff;

        private readonly CarController _carController;
        private readonly InputGameController _inputGameController;
        private readonly AbilitiesController _abilitiesController;
        private readonly TapeBackgroundController _tapeBackgroundController;

        private readonly AbilitiesContext _abilitiesContext;
        private readonly GameUIController _gameUIController;


        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController(profilePlayer.CurrentCar, _rightMoveDiff, _leftMoveDiff);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesContext = CreateAbilitiesContext(placeForUi, _carController, profilePlayer.CurrentCar);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);

            _gameUIController = CreateGameUIController(placeForUi, profilePlayer);
            //ServicesRoster.AnalyticsManager.SendGameStarted();
        }


        private TapeBackgroundController CreateTapeBackground(SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);

            return tapeBackgroundController;
        }

        private InputGameController CreateInputGameController(ProfilePlayer profilePlayer,
            SubscriptionProperty<float> leftMoveDiff, SubscriptionProperty<float> rightMoveDiff)
        {
            var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);

            return inputGameController;
        }

        private AbilitiesContext CreateAbilitiesContext(Transform placeForUi, IAbilityActivator carController, CarModel carModel)
        {
            var _abilitiesContext = new AbilitiesContext(placeForUi, carController, carModel);
            AddContext(_abilitiesContext);

            return _abilitiesContext;
        }
        private CarController CreateCarController(CarModel model, SubscriptionProperty<float> _rightMoveDiff, SubscriptionProperty<float> _leftMoveDiff)
        {
            var carController = new CarController(model, _rightMoveDiff, _leftMoveDiff);
            AddController(carController);

            return carController;
        }

        private GameUIController CreateGameUIController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            var gameUIController = new GameUIController(placeForUi, profilePlayer);
            AddController(gameUIController);

            return gameUIController;
        }
    }
}
