using UnityEngine;

namespace Features.AbilitySystem
{
    internal interface IAbilityActivator
    {
        GameObject ViewGameObject { get; }
        Transform ViewCannon { get; }
    }
}
