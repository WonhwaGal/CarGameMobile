using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAds;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Button _buttonShed;

        [SerializeField] private string _buyItemId;


        public void Init(UnityAction startGame, UnityAction goSettings, UnityAction watchAds, UnityAction goShed, UnityAction<string> buyItem)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(goSettings);
            _buttonAds.onClick.AddListener(watchAds);
            _buttonShed.onClick.AddListener(goShed);
            _buttonBuy.onClick.AddListener(() => buyItem(_buyItemId));
        }

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonAds.onClick.RemoveAllListeners();
            _buttonShed.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
        }


    }
}
