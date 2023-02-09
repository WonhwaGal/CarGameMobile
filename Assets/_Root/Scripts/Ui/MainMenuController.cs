using Profile;
using Tool;
using UnityEngine;
using Services.Ads.UnityAds;
using Services.IAP;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame, GoToSettings, WatchRewardedAd, BuyItem);
            UnityAdsService.Instance.RewardedPlayer.Finished += ReceiveCrystals;
            IAPService.Instance.PurchaseSucceed.AddListener(ItemPurchased);
        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;
        private void GoToSettings() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;
        private void WatchRewardedAd() => UnityAdsService.Instance.RewardedPlayer.Play();
        private void ReceiveCrystals() => Debug.LogWarning("5 crystals received");
        private void BuyItem(string id) => IAPService.Instance.Buy(id);
        private void ItemPurchased() => Debug.LogWarning("Purchase successfully made");

        protected override void OnDispose()
        {
            UnityAdsService.Instance.RewardedPlayer.Finished -= ReceiveCrystals;
            IAPService.Instance.PurchaseSucceed.RemoveListener(ItemPurchased);
        }
    }
}
