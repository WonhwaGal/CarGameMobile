
namespace Features.Shed.Upgrade
{
    internal class JumpUpgradeHandler : IUpgradeHandler
    {
        private readonly float _jumpForce;

        public JumpUpgradeHandler(float jumpForce) =>
            _jumpForce = jumpForce;

        public void Upgrade(IUpgradable upgradable)
        {
            upgradable.JumpHeight += _jumpForce;
        }

    }
}
