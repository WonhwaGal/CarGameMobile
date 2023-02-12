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

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _leftMoveDiff = new SubscriptionProperty<float>();
            _rightMoveDiff = new SubscriptionProperty<float>();

            _carController = CreateCarController(profilePlayer, _rightMoveDiff);
            _inputGameController = CreateInputGameController(profilePlayer, _leftMoveDiff, _rightMoveDiff);
            _abilitiesController = CreateAbilitiesController(placeForUi, _carController, profilePlayer);
            _tapeBackgroundController = CreateTapeBackground(_leftMoveDiff, _rightMoveDiff);

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

        private CarController CreateCarController(ProfilePlayer profilePlayer, SubscriptionProperty<float> _rightMoveDiff)
        {
            var carController = new CarController(profilePlayer, _rightMoveDiff);
            AddController(carController);

            return carController;
        }

        private AbilitiesController CreateAbilitiesController(Transform placeForUi, IAbilityActivator abilityActivator, ProfilePlayer profilePlayer)
        {
            var abilitiesController = new AbilitiesController(placeForUi, abilityActivator, profilePlayer);
            AddController(abilitiesController);

            return abilitiesController;
        }
    }
}
