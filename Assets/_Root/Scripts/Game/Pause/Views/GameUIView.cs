using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Game
{
    public class GameUIView : MonoBehaviour
    {
        [field: SerializeField] public Button PauseSubMenuButton { get; private set; }

        public void Init(UnityAction openPauseMenu)
        {
            PauseSubMenuButton.onClick.AddListener(openPauseMenu);
        }

        private void OnDisable()
        {
            PauseSubMenuButton.onClick.RemoveAllListeners();
        }
    }
}

