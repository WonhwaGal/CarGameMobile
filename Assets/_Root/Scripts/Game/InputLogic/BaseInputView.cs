using JoostenProductions;
using Tool;
using UnityEngine;
using Tool.Pause;

namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour, IPauseHandler
    {
        protected float Speed;

        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;

        protected bool IsPaused;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
            PauseController.Instance.Register(this);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
            PauseController.Instance.UnRegister(this);
        }


        protected abstract void Move();


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            Speed = speed;
        }

        protected void OnLeftMove(float value) =>
            _leftMove.Value = value;

        protected void OnRightMove(float value) =>
            _rightMove.Value = value;

        void IPauseHandler.SetPaused(bool isPaused)
        {
            IsPaused = isPaused;
        }
    }
}
