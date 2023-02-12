using UnityEngine;
using Tool;
using Profile;

namespace Game.Car
{
    internal class CarView : MonoBehaviour
    {
        [SerializeField] private Transform _cannon;
        [SerializeField] private GameObject _bumper;
        [SerializeField] private Transform[] _tires;

        private float _speed;

        public Transform Cannon { get => _cannon; set => _cannon = value; }

        public void Init(ProfilePlayer profile, SubscriptionProperty<float> _rightMoveDiff)
        {
            _bumper.SetActive(profile.CurrentCar.Shield);
            _speed = profile.CurrentCar.Speed * 3;
            _rightMoveDiff.SubscribeOnChange(DriveToRight);
        }
        private void DriveToRight(float rightDiff)
        {
            Debug.Log(rightDiff);
            foreach (Transform tire in _tires)
            {
                tire.Rotate(0,0, -rightDiff * _speed);
            }
        }
    }
}
