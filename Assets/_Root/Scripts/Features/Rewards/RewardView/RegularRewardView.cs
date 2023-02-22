using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Rewards
{
    internal class RegularRewardView : MonoBehaviour
    {
        public string CurrentSlotInActiveKey { get; protected set; }
        public string TimeGetRewardKey { get; protected set; }

        public float TimeCooldown { get; protected set; }  // 86400 for  daily
        public float TimeDeadline { get; protected set; }  // 172800 for daily
        public string TimespanName { get; protected set; }

        [field: Header("Settings Rewards")]
        [field: SerializeField] public List<Reward> Rewards { get; private set; }

        [field: Header("Ui Elements")]
        [field: SerializeField] public TMP_Text TimerNewReward { get; private set; }
        [field: SerializeField] public Transform MountRootSlotsReward { get; private set; }
        [field: SerializeField] public ContainerSlotRewardView ContainerSlotRewardPrefab { get; private set; }
        [field: SerializeField] public Button GetRewardButton { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }
        [field: SerializeField] public Button CloseButton { get; private set; }
        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetReward
        {
            get
            {
                string data = PlayerPrefs.GetString(TimeGetRewardKey);
                return !string.IsNullOrEmpty(data) ? DateTime.Parse(data) : null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetRewardKey);
            }
        }
    }
}

