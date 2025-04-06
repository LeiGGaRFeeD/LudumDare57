using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    // Ссылка на ScriptableObject с настройками кислорода
    public OxygenSettings oxygenSettings;

    // Слайдер для отображения уровня кислорода
    public Slider oxygenSlider;

    // Текущий уровень кислорода
    private int currentOxygen;

    // Таймер для уменьшения кислорода каждую секунду
    private float timer = 1f;

    // Флаг окончания кислорода
    private bool isOutOfOxygen = false;

    private void Start()
    {
        // Получаем уровень прокачки из PlayerPrefs
        int oxygenLevel = PlayerPrefs.GetInt("OxygenLvl", 1); // Если значение не сохранено, используем уровень 1

        // Устанавливаем начальное значение кислорода на основе уровня прокачки
        currentOxygen = oxygenSettings.GetInitialOxygen(oxygenLevel);

        // Настраиваем слайдер (если он установлен)
        if (oxygenSlider != null)
        {
            oxygenSlider.maxValue = currentOxygen;
            oxygenSlider.value = currentOxygen;
        }
    }

    private void Update()
    {
        if (isOutOfOxygen) return; // Если кислород закончился, ничего не делаем

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 1f; // Сбрасываем таймер

            DecreaseOxygen(1); // Уменьшаем уровень кислорода на 1 каждую секунду
        }
    }

    private void DecreaseOxygen(int amount)
    {
        currentOxygen -= amount;

        if (oxygenSlider != null)
        {
            oxygenSlider.value = currentOxygen; // Обновляем значение слайдера
        }

        if (currentOxygen <= 0)
        {
            currentOxygen = 0;
            OnOutOfOxygen(); // Выполняем действия при окончании кислорода
        }
    }

    private void OnOutOfOxygen()
    {
        isOutOfOxygen = true;

        Debug.Log("Кислород закончился! Игрок погиб или требуется действие.");

        // Здесь можно добавить логику окончания игры или других действий:
        // Например, вызвать GameOver экран или перезапустить уровень.

        // Пример:
        // GameManager.Instance.GameOver();
    }

    public void AddOxygen(int amount)
    {
        currentOxygen += amount;

        if (currentOxygen > oxygenSlider.maxValue)
            currentOxygen = (int)oxygenSlider.maxValue;

        if (oxygenSlider != null)
            oxygenSlider.value = currentOxygen;

        isOutOfOxygen = false; // Если добавили кислород после окончания, снова активируем систему
    }
}
