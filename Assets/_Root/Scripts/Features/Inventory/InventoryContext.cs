using Tool;
using UnityEngine;
using Features.Inventory;
using Features.Inventory.Items;
using System.Diagnostics.CodeAnalysis;

using Object = UnityEngine.Object;
using System;

internal class InventoryContext : BaseContext
{
    private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
    private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

    public InventoryContext([NotNull] Transform placeForUi, [NotNull] IInventoryModel model)
    {
        if (placeForUi == null) throw new ArgumentNullException(nameof(placeForUi));

        CreateController(placeForUi, model);
    }

    private InventoryController CreateController(Transform placeForUi, IInventoryModel model)
    {
        IInventoryView view = LoadInventoryView(placeForUi);
        IItemsRepository repository = CreateInventoryRepository();
        var inventoryController = new InventoryController(view, repository, model);
        AddController(inventoryController);

        return inventoryController;
    }
    private ItemsRepository CreateInventoryRepository()
    {
        ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);
        var repository = new ItemsRepository(itemConfigs);
        AddRepository(repository);

        return repository;
    }

    private InventoryView LoadInventoryView(Transform placeForUi)
    {
        GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
        GameObject objectView = Object.Instantiate(prefab, placeForUi);
        AddGameObject(objectView);

        return objectView.GetComponent<InventoryView>();
    }
}
