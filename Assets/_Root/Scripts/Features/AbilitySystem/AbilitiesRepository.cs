using System.Collections.Generic;
using Features.AbilitySystem.Abilities;
using Profile;

namespace Features.AbilitySystem
{
    internal interface IAbilitiesRepository : IRepository
    {
        IReadOnlyDictionary<string, IAbility> Items { get; }
    }

    internal class AbilitiesRepository : BaseRepository<string, IAbility, AbilityItemConfig>
    {
        public AbilitiesRepository(IEnumerable<AbilityItemConfig> configs, ProfilePlayer profilePlayer) : base(configs, profilePlayer)
        {
            UnityEngine.Debug.Log("abilities repository => Current Jump ="+ProfilePlayer.CurrentCar.JumpHeight);
        }

        protected override string GetKey(AbilityItemConfig config) => config.Id;

        protected override IAbility CreateItem(AbilityItemConfig config) =>
            config.Type switch
            {
                AbilityType.Gun => new GunAbility(config),
                AbilityType.Jump => new JumpAbility(config, ProfilePlayer),
                _ => StubAbility.Default
            };
    }
}
