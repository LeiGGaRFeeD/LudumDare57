using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedObstacleAwareSpawner : MonoBehaviour
{
    // Массив префабов для спавна
    public GameObject[] prefabsToSpawn;

    // Размеры области спавна
    public float spawnAreaWidth = 10f;
    public float spawnAreaHeight = 10f;

    // Радиус проверки на наличие препятствий
    public float checkRadius = 1f;

    // Максимальное количество попыток найти свободное место
    public int maxAttempts = 30;

    // Настройки автоматического спавна
    public bool autoSpawn = true;
    public float spawnInterval = 2f;
    public int maxSpawnedObjects = 10;

    // Список созданных объектов
    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        if (autoSpawn)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    // Корутина для автоматического спавна
    private IEnumerator SpawnRoutine()
    {
        while (autoSpawn)
        {
            // Проверяем, не превышено ли максимальное количество объектов
            if (spawnedObjects.Count < maxSpawnedObjects)
            {
                GameObject spawned = SpawnPrefab();
                if (spawned != null)
                {
                    spawnedObjects.Add(spawned);
                }
            }

            // Очищаем список от уничтоженных объектов
            for (int i = spawnedObjects.Count - 1; i >= 0; i--)
            {
                if (spawnedObjects[i] == null)
                {
                    spawnedObjects.RemoveAt(i);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Метод для спавна префаба в случайной точке без препятствий
    public GameObject SpawnPrefab()
    {
        // Проверяем, есть ли префабы для спавна
        if (prefabsToSpawn.Length == 0)
        {
            Debug.LogError("Нет префабов для спавна!");
            return null;
        }

        // Выбираем случайный префаб из массива
        GameObject prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];

        int attempts = 0;
        Vector2 spawnPosition;
        bool positionClear = false;

        // Пытаемся найти свободную позицию
        while (!positionClear && attempts < maxAttempts)
        {
            // Генерируем случайную позицию в пределах зоны спавна
            spawnPosition = GenerateRandomPosition();

            // Проверяем, нет ли объектов с тегом "Obstacle" в этой позиции
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, checkRadius);

            positionClear = true;

            // Проверяем все найденные коллайдеры
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Obstacle"))
                {
                    positionClear = false;
                    break;
                }
            }

            // Если позиция свободна, создаем префаб
            if (positionClear)
            {
                return Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }

            attempts++;
        }

        Debug.LogWarning("Не удалось найти свободное место для спавна после " + maxAttempts + " попыток.");
        return null;
    }

    // Метод для генерации случайной позиции в пределах зоны спавна
    private Vector2 GenerateRandomPosition()
    {
        float x = transform.position.x + Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float y = transform.position.y + Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);
        return new Vector2(x, y);
    }

    // Визуализация зоны спавна в редакторе (для удобства настройки)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaWidth, spawnAreaHeight, 0));
    }
}
