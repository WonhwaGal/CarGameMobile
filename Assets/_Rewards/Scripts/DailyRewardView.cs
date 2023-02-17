using UnityEngine;

namespace Rewards
{
    public class InspectorReadOnlyAttribute : PropertyAttribute
    {

    }
    internal sealed class DailyRewardView : RegularRewardView
    {
        [field: Header("Settings Time Get Reward")]
        [SerializeField] private string _timespanName = "Day";

        [SerializeField]
        [InspectorReadOnly] private float _cooldownTime = 86400;

        [SerializeField]
        [InspectorReadOnly] private float _deadlineTime = 86400 * 2;


        private void Awake()
        {
            TimespanName = _timespanName;

            TimeCooldown = _cooldownTime;
            TimeDeadline = _deadlineTime;
        }
    }
}
