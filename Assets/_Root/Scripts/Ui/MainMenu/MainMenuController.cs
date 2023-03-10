using Profile;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/Ui/MainMenuTween");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private readonly CustomLogger _logger;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GoToSettings, GoShed, WatchRewardedAd, BuyItem, OpenDailyReward, ExitGame);

            SubscribeAds();
            SubscribeIAP();

            _logger = LoggerFactory.Create<MainMenuController>();
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
        private void OpenDailyReward() => _profilePlayer.CurrentState.Value = GameState.DailyReward;
        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

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

        private void OnAdsFinished() => _logger.Log("Ads reward is granted");
        private void OnAdsCancelled() => _logger.Warning("Process was interrupted. Reward is not granted.");
        private void OnIAPSucceed() => _logger.Log("Purchase succeed");
        private void OnIAPFailed() => _logger.Warning("Purchase failed");
    }
}
