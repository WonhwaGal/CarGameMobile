using Profile;
using System;
using System.Collections.Generic;
using Features.Shed.Upgrade;
using JetBrains.Annotations;

namespace Features.Shed
{
    internal interface IShedController
    {
    }

    internal class ShedController : BaseController, IShedController
    {
        private readonly IShedView _view;
        //private readonly BaseContext _inventoryContext;
        private readonly ProfilePlayer _profilePlayer;
        //private readonly InventoryContext _inventoryContext;
        private readonly IUpgradeHandlersRepository _upgradeHandlersRepository;

        private readonly CustomLogger _logger;


        public ShedController(
            [NotNull] IShedView view,
            [NotNull] IUpgradeHandlersRepository upgradeHandlersRepository,
            [NotNull] ProfilePlayer profilePlayer)
        {
            _profilePlayer
                = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _view
                = view ?? throw new ArgumentNullException(nameof(view));

            _upgradeHandlersRepository
                = upgradeHandlersRepository ?? throw new ArgumentNullException(nameof(upgradeHandlersRepository));

            _view.Init(Apply, Back);

            _logger = LoggerFactory.Create<ShedController>();
        }

        protected override void OnDispose()
        {
            _view.Deinit();
            base.OnDispose();
        }

        private void Apply()
        {
            _profilePlayer.CurrentCar.Restore();

            UpgradeWithEquippedItems(
                _profilePlayer.CurrentCar,
                _profilePlayer.Inventory.EquippedItems,
                _upgradeHandlersRepository.Items);

            _profilePlayer.CurrentState.Value = GameState.Start;
            _logger.Log($"Apply. Speed: {_profilePlayer.CurrentCar.Speed}, Jump: {_profilePlayer.CurrentCar.JumpHeight}, Shield {_profilePlayer.CurrentCar.Shield}");
        }

        private void Back()
        {
            _profilePlayer.CurrentState.Value = GameState.Start;
            _logger.Log($"Back. Speed: {_profilePlayer.CurrentCar.Speed}, Jump: {_profilePlayer.CurrentCar.JumpHeight}, Shield {_profilePlayer.CurrentCar.Shield}");
        }


        private void UpgradeWithEquippedItems(
            IUpgradable upgradable,
            IReadOnlyList<string> equippedItems,
            IReadOnlyDictionary<string, IUpgradeHandler> upgradeHandlers)
        {
            foreach (string itemId in equippedItems)
                if (upgradeHandlers.TryGetValue(itemId, out IUpgradeHandler handler))
                    handler.Upgrade(upgradable);
        }
    }
}
