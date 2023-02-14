using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;
using Game.Car;
using System;
using Tool;
using UnityEngine;


namespace Game
{
    internal class AbilitiesContext: BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
        CarModel _carModel;

        public AbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator, CarModel carModel)
        {
            if (placeForUi == null) throw new ArgumentNullException(nameof(placeForUi));
            if (abilityActivator == null) throw new ArgumentNullException(nameof(abilityActivator));

            CreateController(placeForUi, abilityActivator, carModel);
        }

        private AbilitiesController CreateController(Transform placeForUi, IAbilityActivator abilityActivator, CarModel carModel)
        {
            _carModel = carModel;
            IAbilitiesView view = LoadView(placeForUi);
            AbilityItemConfig[] itemConfigs = LoadAbilityItemConfigs();
            IAbilitiesRepository repository = CreateRepository(itemConfigs);

            var abilitiesController = new AbilitiesController(view, repository, itemConfigs, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }


        private AbilityItemConfig[] LoadAbilityItemConfigs() =>
            ContentDataSourceLoader.LoadAbilityItemConfigs(_dataSourcePath);


        private AbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs, _carModel);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }
    }
}


