using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ui
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Button _backButton;

        public void Init(UnityAction goBack) =>
            _backButton.onClick.AddListener(goBack);
        public void OnDestroy() => _backButton.onClick.RemoveAllListeners();

    }
}

