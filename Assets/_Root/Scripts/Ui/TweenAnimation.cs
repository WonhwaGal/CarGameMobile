using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;


namespace Ui
{
    public class TweenAnimation : MonoBehaviour
    {
        [Space(65)]
        [Header("Components")]
        [SerializeField] private List<GameObject> _buttons;
        [SerializeField] private List<RectTransform> _rectTransforms;
        private List<Vector3> _startScales = new List<Vector3>();

        [Header("Settings")]
        [SerializeField] private float _duration = 2.5f;
        [SerializeField] private float _strength = 12f;
        [SerializeField] private int _loopCount;
        [SerializeField] private Ease _moveEase;


        private void OnValidate() => InitComponents();
        private void Awake() => InitComponents();
        private void InitComponents()
        {
            if (_buttons.Count == _rectTransforms.Count)
            {
                for (int i = 0; i < _buttons.Count; i++)
                {
                    _rectTransforms[i] ??= _buttons[i].GetComponent<RectTransform>();
                    _startScales.Add(_rectTransforms[i].localScale);
                }
            }
        }

        [ContextMenu(nameof(Start_Animations))]
        public void Start_Animations()
        {
            Stop_All_Animations();

            for (int i = 0; i < _rectTransforms.Count; i++)
            {
                int number = UnityEngine.Random.Range(0, 3);
                AssignAnimation(_rectTransforms[i], number);
            }


        }
        private void AssignAnimation(RectTransform rect, int number)
        {
            switch (number)
            {
                case 0:
                    rect.DOShakeRotation(_duration, Vector3.forward * _strength)
                        .SetLoops(_loopCount)
                        .SetEase(_moveEase);
                    break;
                case 1:
                    rect.DOScale(rect.localScale * 1.2f, _duration * 2)
                        .SetLoops(_loopCount)
                        .SetEase(_moveEase);

                    break;
                case 2:
                    rect.DOShakeRotation(_duration, Vector3.forward * _strength / 2)  // do something else
                        .SetLoops(_loopCount)
                        .SetEase(_moveEase);
                    break;
            }
        }

        [ContextMenu(nameof(Stop_All_Animations))]
        public void Stop_All_Animations()
        {
            for (int i = 0; i < _rectTransforms.Count; i++)
            {
                _rectTransforms[i].DOKill(true);
                _rectTransforms[i].localScale = _startScales[i];
            }
        }

        private void OnDisable()
        {
            Stop_All_Animations();
        }
    }
}

