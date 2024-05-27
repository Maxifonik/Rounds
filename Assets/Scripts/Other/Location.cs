using UnityEngine;

public class Location : MonoBehaviour
{
    // ������ ����� ��������� �������
    private PlayerSpawnPoint[] _spawnPoints;

    // �������� ��� ������� � ������� �����
    public PlayerSpawnPoint[] SpawnPoints => _spawnPoints;

    public void Init()
    {
        // �������� ��� ����� �� �������� ��������
        _spawnPoints = GetComponentsInChildren<PlayerSpawnPoint>();

        // ������� ��������� �� ������������� � �������
        Debug.Log("Location Init");
    }
}