using UnityEngine;
using System.Collections.Generic;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        public static AnalyticsManager Instance { get; private set; }

        private IAnalyticsService[] _services;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this.gameObject);

            _services = new IAnalyticsService[]
{
                new UnityAnalyticsService()
};
        }


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendGameStarted() =>
            SendEvent("GameStarted");
        private void SendEvent(string eventName)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName);
        }
        private void SendEvent(string eventName, Dictionary<string, object> data)
        {
            for (int i = 0; i < _services.Length; i++)
                _services[i].SendEvent(eventName, data);
        }
    }
}
