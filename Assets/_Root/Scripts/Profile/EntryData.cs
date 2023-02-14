using Features.AbilitySystem.Abilities;
using Profile;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(EntryData), menuName = "Configs/" + nameof(EntryData) + "Cfg", order = 0)]
public class EntryData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }

    [SerializeField] private AbilityItemConfig JumpAbility;
    [field: SerializeField] public GameState InitialGameState { get; private set; }

    public float JumpHeight { get => JumpAbility.Value; }
}
