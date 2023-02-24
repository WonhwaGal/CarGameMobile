using Game.Car;
using Profile;
using Tool;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputGameController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Input/KeyInput");
        private readonly BaseInputView _view;


        public InputGameController(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            ProfilePlayer profilePlayer)
        {
            _view = LoadView();
            _view.Init(leftMove, rightMove, profilePlayer.CurrentCar.Speed, profilePlayer.PauseModel);
        }


        private BaseInputView LoadView()
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab);
            AddGameObject(objectView);

            return objectView.GetComponent<BaseInputView>();
        }
    }
}
