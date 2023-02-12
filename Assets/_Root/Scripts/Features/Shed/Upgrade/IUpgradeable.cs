namespace Features.Shed.Upgrade
{
    internal interface IUpgradable
    {
        float Speed { get; set; }
        float JumpHeight { get; set; }
        bool Shield { get; set; }
        void Restore();
    }
}
