namespace Services.Analytics
{
    internal interface IAnalyticsManager
    {
        void SendMainMenuOpened();
        void SendGameStarted();
        void SendTransaction(string id, decimal count, string currency);
    }
}

