using System.Collections.Generic;
using UnityEngine;

namespace Services.Analytics.UnityAnalytics
{
    internal class UnityAnalyticsService : IAnalyticsService
    {
        public void SendEvent(string eventName)
        {
            UnityEngine.Analytics.Analytics.CustomEvent(eventName);
            //Debug.Log($"Sending an event: {eventName}");
        }

        public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
            UnityEngine.Analytics.Analytics.CustomEvent(eventName, eventData);

        public void SendTransaction(string id, decimal count, string currency) =>
            UnityEngine.Analytics.Analytics.Transaction(id, count, currency);
    }
}
