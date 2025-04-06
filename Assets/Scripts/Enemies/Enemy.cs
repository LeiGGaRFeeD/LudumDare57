using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private GameObject _redSolid;
    private bool _isGameOver;

    void Start()
    {
        // Автоматически находим объект с тегом "RedSolid"
        _redSolid = GameObject.FindWithTag("RedSolid");
        if (_redSolid != null)
        {
            _redSolid.SetActive(false); // Выключаем при старте
        }
        else
        {
            Debug.LogError("Не найден объект с тегом 'RedSolid'");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isGameOver) return;

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        _isGameOver = true;

        // Активируем эффект и останавливаем игру
        if (_redSolid != null)
        {
            _redSolid.SetActive(true);
        }
        Time.timeScale = 0;

        // Ждем 2 секунды реального времени
        yield return new WaitForSecondsRealtime(2f);

        // Возвращаем нормальную скорость времени и загружаем сцену
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
