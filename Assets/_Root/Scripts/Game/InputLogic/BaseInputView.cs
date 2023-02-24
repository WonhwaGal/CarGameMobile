using JoostenProductions;
using Tool;
using UnityEngine;


namespace Game.InputLogic
{
    internal abstract class BaseInputView : MonoBehaviour, IPauseHandler
    {
        protected float Speed;

        private SubscriptionProperty<float> _leftMove;
        private SubscriptionProperty<float> _rightMove;

        private PauseMenuModel _pauseModel;
        protected bool IsPaused;

        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
            _pauseModel.Register(this);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(Move);
            _pauseModel.UnRegister(this);
        }


        protected abstract void Move();


        public virtual void Init(
            SubscriptionProperty<float> leftMove,
            SubscriptionProperty<float> rightMove,
            float speed,
            PauseMenuModel pauseModel)
        {
            _leftMove = leftMove;
            _rightMove = rightMove;
            Speed = speed;
            _pauseModel = pauseModel;
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
