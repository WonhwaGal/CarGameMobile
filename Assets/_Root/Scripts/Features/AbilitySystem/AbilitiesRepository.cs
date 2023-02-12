using System.Collections.Generic;
using Features.AbilitySystem.Abilities;
using Game.Car;
using Profile;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, AbilityItemConfig>
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs, CarModel model) : base(configs, model)
        {
            UnityEngine.Debug.Log("abilities repository => Current Jump ="+ model.JumpHeight);
        }

        protected override string GetKey(AbilityItemConfig config) => config.Id;

        protected override IAbility CreateItem(AbilityItemConfig config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                AbilityType.Jump => new JumpAbility(config, CarModel),
                _ => StubAbility.Default
            };
    }
}
