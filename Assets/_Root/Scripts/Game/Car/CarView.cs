using UnityEngine;
using Tool;

namespace Game.Car
{
    internal class CarView : MonoBehaviour
    {
        [SerializeField] private Transform _cannon;
        [SerializeField] private GameObject _bumper;
        [SerializeField] private Transform[] _tires;

        private float _speed;
        private const float _tireSpinSpeedMultiplier = 3;
        private SubscriptionProperty<float> _rightMoveDiff;
        public Transform Cannon { get => _cannon; set => _cannon = value; }


        public void Init(CarModel model, SubscriptionProperty<float> rightMoveDiff)
        {
            _bumper.SetActive(model.Shield);
            _speed = model.Speed * _tireSpinSpeedMultiplier;
            _rightMoveDiff = rightMoveDiff;
            _rightMoveDiff.SubscribeOnChange(DriveToRight);

        }
        private void DriveToRight(float rightDiff)
        {
            foreach (Transform tire in _tires)
            {
                tire.Rotate(0,0, -rightDiff * _speed);
            }
        }

        private void OnDestroy()
        {
            _rightMoveDiff.UnSubscribeOnChange(DriveToRight);
        }
    }
}
