using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeConfig", menuName = "Configs/Upgrade Config")]
public class UpgradeConfig : ScriptableObject
{
    [System.Serializable]
    public class UpgradeSettings
    {
        public float baseValue;
        public float upgradeStep;
        public int maxLevel;
        public int baseUpgradeCost;
        public float costMultiplier = 1.5f;
    }

    [Header("Oxygen Settings")]
    public UpgradeSettings oxygen = new UpgradeSettings()
    {
        baseValue = 100f,
        upgradeStep = 25f,
        maxLevel = 5,
        baseUpgradeCost = 100
    };

    [Header("Swim Speed Settings")]
    public UpgradeSettings swimSpeed = new UpgradeSettings()
    {
        baseValue = 3f,
        upgradeStep = 0.5f,
        maxLevel = 5,
        baseUpgradeCost = 150
    };
}
