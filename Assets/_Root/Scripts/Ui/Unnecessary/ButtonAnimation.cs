using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private List<RectTransform> _children;

    [Header("Settings")]
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private Ease _moveEase;

    private Vector3 _punch;

    private void OnValidate() => InitComponents();
    private void Awake() => InitComponents();

    private void Start() => _button.onClick.AddListener(OnButtonClick);
    private void OnDestroy() => _button.onClick.RemoveAllListeners();

    private void InitComponents()
    {
        _button ??= GetComponent<Button>();
        _rectTransform ??= GetComponent<RectTransform>();
        _punch = _rectTransform.transform.position + new Vector3(2,0,0);
    }

    private void OnButtonClick() => ActivateAnimation();

    private void ActivateAnimation()
    {
        _rectTransform.DOPunchRotation(_punch, _duration, 2, 0.9f).SetEase(_moveEase);
        foreach (var child in _children)
        {
            child.DOPunchRotation(_punch, _duration, 1, 0.9f).SetEase(_moveEase);
        }
    }
}
