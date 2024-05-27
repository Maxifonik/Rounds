using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviourPunCallbacks
{
    // ������ ��������� �������
    [SerializeField] private PlayerSpawner _playerSpawner;

    // ����������� ��������� ������ ��� ������� �����
    public static RoomController Instance { get; private set; }

    // ����������, ����� MonoBehaviour �������
    public override void OnEnable()
    {
        //  �������� ������� ���������� ������
        base.OnEnable();

        // ������������ ������� sceneLoaded
        // �������� ����� SpawnPlayerSpawner()
        SceneManager.sceneLoaded += SpawnPlayerSpawner;
    }
    // ����������, ����� MonoBehaviour ��������
    public override void OnDisable()
    {
        // �������� ������� ���������� ������
        base.OnDisable();

        // ������������ �� ������� sceneLoaded
        SceneManager.sceneLoaded -= SpawnPlayerSpawner;
    }
    // ����������� ������ � MonoBehaviour
    private void Awake()
    {
        // ���� ��������� ������ ����������
        if (Instance)
        {
            // ���������� ���� ������
            Destroy(gameObject);
        }
        // �� ���������� ������ ��� �������� ����� �����
        DontDestroyOnLoad(gameObject);

        // ��������� ���� ��������� ��� ��������� ����������
        Instance = this;
    }
    // ��������� ��������� ������� ����� �������� �����
    private void SpawnPlayerSpawner(Scene scene, LoadSceneMode loadSceneMode)
    {
        // ���� �� � ������� �����
        if (scene.name == ScenesLoader.GameSceneName)
        {
            // ������ ������ ��������� ������� ����� Photon
            PhotonNetwork.Instantiate(_playerSpawner.name, Vector3.zero, Quaternion.identity);
        }
    }
}