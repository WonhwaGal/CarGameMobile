using Tool;
using System;
using Profile;
using Game.Car;
using UnityEngine;
using Features.AbilitySystem;
using Features.AbilitySystem.Abilities;


namespace Game
{
    internal class AbilitiesContext: BaseContext
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Ability/AbilityItemConfigDataSource");
        private CarModel _carModel;
        private PauseMenuModel _pauseMenuModel;
        private ProfilePlayer _profilePlayer;

        public AbilitiesContext(Transform placeForUi, IAbilityActivator abilityActivator, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null) throw new ArgumentNullException(nameof(placeForUi));
            if (abilityActivator == null) throw new ArgumentNullException(nameof(abilityActivator));

            _profilePlayer = profilePlayer ?? throw new ArgumentNullException(nameof(profilePlayer));

            _pauseMenuModel = profilePlayer.PauseModel;

            CreateController(placeForUi, abilityActivator, profilePlayer);
        }


        private AbilitiesController CreateController(Transform placeForUi, IAbilityActivator abilityActivator, ProfilePlayer profilePlayer)
        {
            _carModel = profilePlayer.CurrentCar;
            IAbilitiesView view = LoadView(placeForUi);
            AbilityItemConfig[] itemConfigs = LoadAbilityItemConfigs();
            IAbilitiesRepository repository = CreateRepository(itemConfigs);

            var abilitiesController = new AbilitiesController(view, repository, itemConfigs, abilityActivator, _pauseMenuModel);
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


