using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const float JumpSpeed = 4f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpSpeed, InitialState);
        _mainController = new MainController(_placeForUi, profilePlayer);

        ServicesRoster.AnalyticsManager.SendMainMenuOpened();  

        //if (ServicesRoster.AdsService.IsInitialized) OnAdsinitialized();
        //else ServicesRoster.AdsService.Initialized.AddListener(OnAdsinitialized);
    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        ServicesRoster.AdsService.Initialized.RemoveListener(OnAdsinitialized);
    }
    private void OnAdsinitialized() => ServicesRoster.AdsService.InterstitialPlayer.Play();
}
