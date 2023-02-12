namespace Features.Shed.Upgrade
{
    internal class ShieldUpgradeHandler : IUpgradeHandler
    {
        private readonly bool _shield;

        public ShieldUpgradeHandler(float boolShield)
        {
            if (boolShield > 0) _shield = true;
            else _shield = false;
        }
  
        public void Upgrade(IUpgradable upgradable)
        {
            upgradable.Shield = _shield;
        }

    }
}

