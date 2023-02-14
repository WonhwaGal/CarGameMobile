using Features.Inventory;
using Features.Shed;
using Features.Shed.Upgrade;
using Profile;
using System;
using System.Diagnostics.CodeAnalysis;
using Tool;
using UnityEngine;


namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");


        public ShedContext([NotNull] Transform placeForUi,
                           [NotNull] ProfilePlayer profilePlayer)
        {
            if (profilePlayer == null) throw new ArgumentNullException(nameof(profilePlayer));
            if (placeForUi == null) throw new ArgumentNullException(nameof(placeForUi));

            CreateShedController(placeForUi, profilePlayer);
        }

        private ShedController CreateShedController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            InventoryContext _inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);

            ShedView _view = LoadView(placeForUi);
            UpgradeHandlersRepository _upgradeHandlersRepository = CreateRepository();
            var _shedController = new ShedController(_view, _upgradeHandlersRepository, profilePlayer);
            return _shedController;
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var inventoryContext = new InventoryContext(placeForUi, model);
            AddContext(inventoryContext);

            return inventoryContext;
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

    }

}
