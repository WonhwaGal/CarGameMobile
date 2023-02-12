using System;
using UnityEngine;
using JetBrains.Annotations;
using Game.Car;

namespace Features.AbilitySystem.Abilities
{
    internal class JumpAbility : IAbility
    {
        private readonly AbilityItemConfig _config;
        private float _jumpForce;
        private const float _jumpTreshold = 0.05f;
        public JumpAbility([NotNull] AbilityItemConfig config, [NotNull] CarModel model)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _jumpForce = model.JumpHeight;
        }


        public void Apply(IAbilityActivator activator)
        {
            var projectile = activator.ViewGameObject.GetComponent<Rigidbody2D>();
            if (Mathf.Abs(projectile.velocity.y) < _jumpTreshold)
            {
                Vector3 force = activator.ViewGameObject.transform.up * _jumpForce;    // _config.Value
                projectile.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("Cant jump, velocity up is " + projectile.velocity.y);
            }
        }
    }
}
