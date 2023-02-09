using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics.UnityAnalytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName)
        {
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);
            Debug.Log($"Sending and event {eventName}");
        }
            

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);
    }
}
