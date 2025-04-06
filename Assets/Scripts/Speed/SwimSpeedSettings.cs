using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


[CreateAssetMenu(fileName = "SwimSpeedSettings", menuName = "Game/SwimSpeedSettings")]
public class SwimSpeedSettings : ScriptableObject
{
    [System.Serializable]
    public class SpeedLevelData
    {
        public int level;
        public float swimSpeed;
    }

    public SpeedLevelData[] speedLevels;

    public float GetSwimSpeed(int level)
    {
        foreach (var data in speedLevels)
        {
            if (data.level == level)
            {
                return data.swimSpeed;
            }
        }

    //    Debug.LogWarning($"Уровень {level} не найден! Используется значение по умолчанию: 3.0f.");
        return 3.0f; // Исправленный синтаксис
    }
}
