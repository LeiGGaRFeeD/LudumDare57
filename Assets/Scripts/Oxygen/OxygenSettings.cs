using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[CreateAssetMenu(fileName = "OxygenSettings", menuName = "Game/OxygenSettings")]
public class OxygenSettings : ScriptableObject
{
    [System.Serializable]
    public class OxygenLevelData
    {
        public int level; // Уровень прокачки
        public int initialOxygenAmount; // Начальное количество кислорода
    }

    public OxygenLevelData[] oxygenLevels; // Массив данных для каждого уровня

    // Метод для получения начального значения кислорода по уровню прокачки
    public int GetInitialOxygen(int level)
    {
        foreach (var data in oxygenLevels)
        {
            if (data.level == level)
            {
                return data.initialOxygenAmount;
            }
        }

     //   Debug.LogWarning("Уровень кислорода не найден! Используется значение по умолчанию: 100.");
        return 100; // Значение по умолчанию
    }
}
