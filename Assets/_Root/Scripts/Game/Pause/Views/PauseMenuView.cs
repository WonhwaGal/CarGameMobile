using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class PauseMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button BackToMenuButton { get; private set; }

        public void Init(UnityAction continueGame, UnityAction backToMenu)
        {
            gameObject.SetActive(false);
            ContinueButton.onClick.AddListener(continueGame);
            BackToMenuButton.onClick.AddListener(backToMenu);
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
        }
        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}

