using UnityEngine;

namespace Rewards
{
    internal sealed class DailyRewardView : RegularRewardView
    {
        [Header("PlayerPrefs Settings")]
        [SerializeField]
        [InspectorReadOnly]
        private string _currentSlotInActiveKey = "DailyCurrentSlot";

        [SerializeField]
        [InspectorReadOnly]
        private string _timeGetRewardKey = "DailyTimeGetReward";


        [Header("Settings Time Get Reward")]
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

            CurrentSlotInActiveKey = _currentSlotInActiveKey;
            TimeGetRewardKey = _timeGetRewardKey;
        }
    }
}
