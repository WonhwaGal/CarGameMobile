using Profile;
using Services.Analytics;
using Services.Ads.UnityAds;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        AnalyticsManager.Instance.SendMainMenuOpened();  

        if (UnityAdsService.Instance.IsInitialized) OnAdsinitialized();
        else UnityAdsService.Instance.Initialized.AddListener(OnAdsinitialized);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        UnityAdsService.Instance.Initialized.RemoveListener(OnAdsinitialized);
    }
    private void OnAdsinitialized() => UnityAdsService.Instance.InterstitialPlayer.Play();
}
