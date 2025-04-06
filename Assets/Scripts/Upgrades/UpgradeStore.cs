using UnityEngine;
using UnityEngine.UI;

public class UpgradeStore : MonoBehaviour
{
    [System.Serializable]
    public class UpgradeUIElements
    {
        public Text priceText;
        public Slider progressSlider;
        public Button upgradeButton;
    }

    [Header("Настройки")]
    [SerializeField] private UpgradeCosts _upgradeCosts;
    [SerializeField] private bool _debugIsOn;

    [Header("UI Elements")]
    [SerializeField] private UpgradeUIElements _oxygenUI;
    [SerializeField] private UpgradeUIElements _speedUI;
    [SerializeField] private UpgradeUIElements _capacityUI;

    private const string MONEY_KEY = "money";

    void Start()
    {
        InitializeUI();
        UpdateAllUI();
    }

    private void InitializeUI()
    {
        // Получаем максимальный уровень из конфига
        int maxLevel = _upgradeCosts.costs.Length;

        // Инициализируем слайдеры
        _oxygenUI.progressSlider.maxValue = maxLevel;
        _speedUI.progressSlider.maxValue = maxLevel;
        _capacityUI.progressSlider.maxValue = maxLevel;
    }

    public void UpgradeAir() => Upgrade("OxygenLvl", _oxygenUI);
    public void UpgradeSpeed() => Upgrade("speedLVL", _speedUI);
    public void UpgradeCapacity() => Upgrade("capacityLVL", _capacityUI);

    private void Upgrade(string playerPrefKey, UpgradeUIElements ui)
    {
        int currentLevel = PlayerPrefs.GetInt(playerPrefKey);
        if (currentLevel >= _upgradeCosts.costs.Length)
        {
            Debug.Log("Максимальный уровень достигнут!");
            return;
        }

        UpgradeCosts.UpgradeCostData costData = _upgradeCosts.GetUpgradeCosts(currentLevel + 1);
        int? cost = GetCostByType(playerPrefKey, costData);

        if (TryPurchaseUpgrade(cost))
        {
            PlayerPrefs.SetInt(playerPrefKey, currentLevel + 1);
            UpdateUI(playerPrefKey, ui);
        }
    }

    private int? GetCostByType(string key, UpgradeCosts.UpgradeCostData data)
    {
        return key switch
        {
            "OxygenLvl" => data?.oxygenCost,
            "speedLVL" => data?.speedCost,
            "capacityLVL" => data?.capacityCost,
            _ => null
        };
    }

    private bool TryPurchaseUpgrade(int? cost)
    {
        if (!cost.HasValue) return false;

        int currentMoney = PlayerPrefs.GetInt(MONEY_KEY, 0);
        if (currentMoney >= cost.Value)
        {
            PlayerPrefs.SetInt(MONEY_KEY, currentMoney - cost.Value);
            return true;
        }

        Debug.Log("Недостаточно денег!");
        return false;
    }

    private void UpdateAllUI()
    {
        UpdateUI("OxygenLvl", _oxygenUI);
        UpdateUI("speedLVL", _speedUI);
        UpdateUI("capacityLVL", _capacityUI);
    }

    private void UpdateUI(string key, UpgradeUIElements ui)
    {
        int currentLevel = PlayerPrefs.GetInt(key);
        ui.progressSlider.value = currentLevel;

        // Проверяем максимальный уровень
        if (currentLevel >= _upgradeCosts.costs.Length)
        {
            ui.priceText.text = "MAX";
            ui.upgradeButton.interactable = false;
            return;
        }

        // Получаем стоимость следующего уровня
        UpgradeCosts.UpgradeCostData nextLevelData = _upgradeCosts.GetUpgradeCosts(currentLevel + 1);
        int? cost = GetCostByType(key, nextLevelData);

        ui.priceText.text = cost.HasValue ? $"{cost.Value}$" : "ERROR";
        ui.upgradeButton.interactable = cost.HasValue &&
            PlayerPrefs.GetInt(MONEY_KEY, 0) >= cost.Value;
    }

    void Update()
    {
        if (_debugIsOn && Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.SetInt("OxygenLvl", 0);
            PlayerPrefs.SetInt("speedLVL", 0);
            PlayerPrefs.SetInt("capacityLVL", 0);
            PlayerPrefs.SetInt(MONEY_KEY, 9999);
            UpdateAllUI();
        }
    }
}
