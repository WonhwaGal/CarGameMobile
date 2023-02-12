using Tool;
using Profile;
using UnityEngine;
using Features.AbilitySystem;

namespace Game.Car
{
    internal class CarController : BaseController, IAbilityActivator
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Game/Car");
        private readonly CarView _view;

        public GameObject ViewGameObject => _view.gameObject;
        public Transform ViewCannon => _view.Cannon;

        public CarController(ProfilePlayer profile, SubscriptionProperty<float> _rightMoveDiff)
        {
            _view = LoadView();
            _view.Init(profile, _rightMoveDiff);
        }


        private CarView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<CarView>();
        }
    }
}
