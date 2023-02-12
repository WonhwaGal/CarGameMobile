using System;
using UnityEngine;
using Profile;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal class GunAbility : IAbility
    {
        private readonly AbilityItemConfig _config;
        private Transform _transform;

        public GunAbility([NotNull] AbilityItemConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void Apply(IAbilityActivator activator)
        {
            _transform = activator.ViewCannon;
            var projectile = Object.Instantiate(_config.Projectile, _transform).GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectile.AddForce(force, ForceMode2D.Force);
        }
    }
}
