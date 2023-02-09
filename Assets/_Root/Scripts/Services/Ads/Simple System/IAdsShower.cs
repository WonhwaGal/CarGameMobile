using System;

namespace Services.Ads.UnityAds
{
    internal interface IAdsShower
    {
        void ShowInterstitial();
        void ShowVideo(Action successShow);
    }
}

