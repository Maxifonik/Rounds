using UnityEngine;

public class Location : MonoBehaviour
{
    // Массив точек появления игроков
    private PlayerSpawnPoint[] _spawnPoints;

    // Свойство для доступа к массиву точек
    public PlayerSpawnPoint[] SpawnPoints => _spawnPoints;

    public void Init()
    {
        // Получаем все точки из дочерних объектов
        _spawnPoints = GetComponentsInChildren<PlayerSpawnPoint>();

        // Выводим сообщение об инициализации в консоль
        Debug.Log("Location Init");
    }
}