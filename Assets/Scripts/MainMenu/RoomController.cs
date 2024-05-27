using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{
    // Объект появления игроков
    [SerializeField] private PlayerSpawner _playerSpawner;

    // Статический экземпляр класса для доступа извне
    public static RoomController Instance { get; private set; }

    // Вызывается, когда MonoBehaviour включен
    public override void OnEnable()
    {
        //  Вызываем базовую реализацию метода
        base.OnEnable();

        // Обрабатываем событие sceneLoaded
        // Вызываем метод SpawnPlayerSpawner()
        SceneManager.sceneLoaded += SpawnPlayerSpawner;
    }
    // Вызывается, когда MonoBehaviour отключен
    public override void OnDisable()
    {
        // Вызываем базовую реализацию метода
        base.OnDisable();

        // Отписываемся от события sceneLoaded
        SceneManager.sceneLoaded -= SpawnPlayerSpawner;
    }
    // Выполняется первым в MonoBehaviour
    private void Awake()
    {
        // Если экземпляр класса существует
        if (Instance)
        {
            // Уничтожаем этот объект
            Destroy(gameObject);
        }
        // Не уничтожаем объект при загрузке новой сцены
        DontDestroyOnLoad(gameObject);

        // Сохраняем этот экземпляр как доступный статически
        Instance = this;
    }
    // Запускаем появление игроков после загрузки сцены
    private void SpawnPlayerSpawner(Scene scene, LoadSceneMode loadSceneMode)
    {
        // Если мы в игровой сцене
        if (scene.name == ScenesLoader.GameSceneName)
        {
            // Создаём объект появления игроков через Photon
            PhotonNetwork.Instantiate(_playerSpawner.name, Vector3.zero, Quaternion.identity);
        }
    }
}