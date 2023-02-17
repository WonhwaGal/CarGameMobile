using UnityEngine;

namespace Rewards
{
    internal sealed class WeeklyRewardView : RegularRewardView
    {
        [field: Header("Settings Time Get Reward")]

        [SerializeField] private string _timespanName = "Week";

        [SerializeField]
        [InspectorReadOnly] private float _cooldownTime = 604800;

        [SerializeField]
        [InspectorReadOnly] private float _deadlineTime = 604800 * 2;


        private void Awake()
        {
            TimespanName = _timespanName;

            TimeCooldown = _cooldownTime;
            TimeDeadline = _deadlineTime;
        }
    }
}

