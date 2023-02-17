using UnityEngine;

namespace Rewards
{
    internal class InstallView : MonoBehaviour
    {
        [SerializeField] private RegularRewardView _rewardView;

        private RewardController _rewardController;


        private void Awake() =>
            _rewardController = new RewardController(_rewardView);

        private void Start() =>
            _rewardController.Init();

        private void OnDestroy() =>
            _rewardController.Deinit();
    }
}
