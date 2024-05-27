using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    // Префаб игрока
    [SerializeField] private Player _playerPrefab;

    // Переменная для работы с сетевым представлением объекта
    private PhotonView _photonView;

    // Игровая локация
    private Location _location;

    // Выполняется первым в MonoBehaviour
    private void Awake()
    {
        // Получаем доступ к сетевому представлению объекта
        _photonView = GetComponent<PhotonView>();

        // Находим в сцене объект типа Location
        _location = FindObjectOfType<Location>();
    }
    // Вызывается при запуске игры
    private void Start()
    {
        // Если это наш объект
        // То есть он принадлежит локальному игроку
        if (_photonView.IsMine)
        {
            // Инициализируем локацию
            _location.Init();

            // Вызываем метод SpawnPlayer()
            SpawnPlayer();
        }
    }
    private void SpawnPlayer()
    {
        // Выбираем точку для появления игрока
        // На основе его уникального номера в игре
        PlayerSpawnPoint spawnPoint = _location.SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];

        // Создаём сетевой объект игрока в заданной точке
        PhotonNetwork.Instantiate(_playerPrefab.name, spawnPoint.transform.position, Quaternion.identity);
    }
}