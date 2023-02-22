using UnityEngine;
using UnityEngine.UI;
using Tool.Pause;

namespace Game
{
    public class GameUIView : MonoBehaviour
    {
        [field: SerializeField] public Button ExitButton { get; private set; }
        [field: SerializeField] public Button PauseSubMenuButton { get; private set; }
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button BackToMenuButton { get; private set; }

        [SerializeField] private GameObject _PausePanel;

        public void Init()
        {
            PauseSubMenuButton.onClick.AddListener(() =>
            {
                _PausePanel.SetActive(true);
                PauseController.Instance.SetPaused(true);
            });

            ContinueButton.onClick.AddListener(() =>
            {
                _PausePanel.SetActive(false);
                PauseController.Instance.SetPaused(false);
            });
        }
    }
}

