using Profile;
using UnityEngine;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private EntryData _entryData;
    private float SpeedCar;
    private float JumpHeight;
    private GameState InitialState;

    [SerializeField] private Transform _placeForUi;

    private MainController _mainController;


    private void Start()
    {
        SpeedCar = _entryData.Speed;
        JumpHeight = _entryData.JumpHeight;
        InitialState = _entryData.InitialGameState;
        var profilePlayer = new ProfilePlayer(SpeedCar, JumpHeight, InitialState);
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
