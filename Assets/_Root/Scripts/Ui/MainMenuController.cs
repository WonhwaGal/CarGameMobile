using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/mainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GoToSettings, WatchRewardedAd, GoShed, BuyItem);

            SubscribeAds();
            SubscribeIAP();
        }


        protected override void OnDispose()
        {
            UnsubscribeAds();
            UnsubscribeIAP();
        }

        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() => _profilePlayer.CurrentState.Value = GameState.Game;
        private void GoToSettings() => _profilePlayer.CurrentState.Value = GameState.Settings;
        private void GoShed() => _profilePlayer.CurrentState.Value = GameState.Shed;
        private void WatchRewardedAd() => ServicesRoster.AdsService.RewardedPlayer.Play();
        private void BuyItem(string id) => ServicesRoster.IAPService.Buy(id);


        private void SubscribeAds()
        {
            ServicesRoster.AdsService.RewardedPlayer.Finished += OnAdsFinished;
            ServicesRoster.AdsService.RewardedPlayer.Skipped += OnAdsCancelled;
            ServicesRoster.AdsService.RewardedPlayer.Failed += OnAdsCancelled;
        }
        private void UnsubscribeAds()
        {
            ServicesRoster.AdsService.RewardedPlayer.Finished -= OnAdsFinished;
            ServicesRoster.AdsService.RewardedPlayer.Skipped -= OnAdsCancelled;
            ServicesRoster.AdsService.RewardedPlayer.Failed -= OnAdsCancelled;
        }

        private void SubscribeIAP()
        {
            ServicesRoster.IAPService.PurchaseSucceed.AddListener(OnIAPSucceed);
            ServicesRoster.IAPService.PurchaseFailed.AddListener(OnIAPFailed);
        }
        private void UnsubscribeIAP()
        {
            ServicesRoster.IAPService.PurchaseSucceed.RemoveListener(OnIAPSucceed);
            ServicesRoster.IAPService.PurchaseFailed.RemoveListener(OnIAPFailed);
        }

        private void OnAdsFinished() => Debug.Log("Ads reward is granted");
        private void OnAdsCancelled() => Debug.LogWarning("Process was interrupted. Reward is not granted.");
        private void OnIAPSucceed() => Debug.Log("Purchase succeed");
        private void OnIAPFailed() => Debug.LogWarning("Purchase failed");
    }
}
