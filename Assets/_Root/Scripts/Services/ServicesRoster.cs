using UnityEngine;
using Services.Ads;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;

internal class ServicesRoster : MonoBehaviour
{
    private static ServicesRoster _instance;
    private static ServicesRoster Instance => _instance ??= FindObjectOfType<ServicesRoster>();

    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private IAPService _iapService;
    [SerializeField] private AnalyticsManager _analyticsManager;
    public static IAdsService AdsService => Instance._adsService;
    public static IIAPService IAPService => Instance._iapService;
    public static IAnalyticsManager AnalyticsManager => Instance._analyticsManager;

    private void Awake()
    {
        _instance ??= this;
    }
}
