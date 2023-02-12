using System;
using UnityEngine;
using JetBrains.Annotations;
using Profile;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;
        private float _jumpForce;

        public JumpAbility([NotNull] AbilityItemConfig config, [NotNull] ProfilePlayer profilePlayer)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _jumpForce = profilePlayer.CurrentCar.JumpHeight;
        }


        public void Apply(IAbilityActivator activator)
        {
            var projectile = activator.ViewGameObject.GetComponent<Rigidbody2D>();
            Vector3 force = activator.ViewGameObject.transform.up * _jumpForce;    // _config.Value
            projectile.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
