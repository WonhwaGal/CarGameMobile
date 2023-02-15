using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleScripts
{
    internal class MainWindowMediator : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countAgressionText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [Header("Money Buttons")]
        [SerializeField] private Button _addMoneyButton;
        [SerializeField] private Button _minusMoneyButton;

        [Header("Health Buttons")]
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;

        [Header("Agression Buttons")]
        [SerializeField] private Button _addAgressionButton;
        [SerializeField] private Button _minusAgressionButton;

        [Header("Other Buttons")]
        [SerializeField] private Button _passButton;
        [SerializeField] private Button _fightButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _agression;

        private const int MaxAgressionValue = 5; 

        private Enemy _enemy;


        private void Start()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _agression = CreatePlayerData(DataType.Agression);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _agression);

            Unsubscribe();
        }


        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }


        private void Subscribe()
        {
            _addMoneyButton.onClick.AddListener(IncreaseMoney);
            _minusMoneyButton.onClick.AddListener(DecreaseMoney);

            _addHealthButton.onClick.AddListener(IncreaseHealth);
            _minusHealthButton.onClick.AddListener(DecreaseHealth);

            _addPowerButton.onClick.AddListener(IncreasePower);
            _minusPowerButton.onClick.AddListener(DecreasePower);

            _addAgressionButton.onClick.AddListener(IncreaseAgression);
            _minusAgressionButton.onClick.AddListener(DecreaseAgression);

            _passButton.onClick.AddListener(Pass);
            _fightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe()
        {
            _addMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _minusMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _addHealthButton.onClick.RemoveListener(IncreaseHealth);
            _minusHealthButton.onClick.RemoveListener(DecreaseHealth);

            _addPowerButton.onClick.RemoveListener(IncreasePower);
            _minusPowerButton.onClick.RemoveListener(DecreasePower);

            _addAgressionButton.onClick.RemoveListener(IncreaseAgression);
            _minusAgressionButton.onClick.RemoveListener(DecreaseAgression);

            _passButton.onClick.RemoveListener(Pass);
            _fightButton.onClick.RemoveListener(Fight);
        }


        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseAgression()
        {
            if (_agression.Value < MaxAgressionValue)   IncreaseValue(_agression);
            PassButtonUpdate();
        }
        private void DecreaseAgression() => DecreaseValue(_agression);

        private void IncreaseValue(PlayerData playerData) => AddToValue(1, playerData);
        private void DecreaseValue(PlayerData playerData) => AddToValue(-1, playerData);

        private void AddToValue(int addition, PlayerData playerData)
        {
            if (addition < 0)
            {
                if (playerData.Value == 0)
                {
                    return;
                }
            }
            playerData.Value += addition;
            ChangeDataWindow(playerData);
        }

        private void PassButtonUpdate()
        {
            if (_agression.Value >= 3) _passButton.gameObject.SetActive(false);
        }

        private void ChangeDataWindow(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F} {value}";

            int enemyPower = _enemy.CustomCalcPower();
            _countPowerEnemyText.text = $"Enemy Power {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Money => _countMoneyText,
                DataType.Health => _countHealthText,
                DataType.Power => _countPowerText,
                DataType.Agression => _countAgressionText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };


        private void Fight()
        {
            int enemyPower = _enemy.CustomCalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }
        private void Pass()
        {
            Debug.Log($"<color=#FF9D00>You decided to pass.</color>");
        }
    }
}
