using UnityEngine;

namespace Game.InputLogic
{
    internal class KeyInput : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 0.2f;
        private float inputX;


        protected override void Move()
        {
            inputX = Input.GetAxis("Horizontal");
            float speed = Speed * _inputMultiplier * Time.deltaTime;
            if (inputX > 0)
            {
                OnRightMove(speed);
            }
            else if (inputX < 0)
            {
                OnLeftMove(speed);
            }
        }
    }
}

