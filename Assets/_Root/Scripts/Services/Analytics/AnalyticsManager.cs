using UnityEngine;
using System.Collections.Generic;
using Services.Analytics.UnityAnalytics;

namespace Services.Analytics
{
    internal class AnalyticsManager : MonoBehaviour, IAnalyticsManager
    {
        private IAnalyticsService[] _services;

        private void Awake()
        {
            _services = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }


        public void SendMainMenuOpened() =>
            SendEvent("MainMenuOpened");

        public void SendGameStarted() =>
            SendEvent("GameStarted");

        public void SendTransaction(string id, decimal count, string currency)
        {
            for (int i = 0; i < _services.Length; i++)
            {
                _services[i].SendTransaction(id, count, currency);
            }
            Debug.Log($"Send transaction {id}");
        }
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
