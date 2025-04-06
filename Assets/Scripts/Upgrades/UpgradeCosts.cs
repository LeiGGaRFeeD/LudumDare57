using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeCosts", menuName = "Game/Upgrade Costs")]
public class UpgradeCosts : ScriptableObject
{
    [System.Serializable]
    public class UpgradeCostData
    {
        public int level; // Уровень улучшения
        public int oxygenCost;
        public int speedCost;
        public int capacityCost;
    }

    public UpgradeCostData[] costs;

    public UpgradeCostData GetUpgradeCosts(int targetLevel)
    {
        foreach (var data in costs)
        {
            if (data.level == targetLevel)
            {
                return data;
            }
        }

        Debug.LogWarning($"Не найдены данные для уровня {targetLevel}");
        return null;
    }
}
