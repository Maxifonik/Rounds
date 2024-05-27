using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    // ������ ������
    [SerializeField] private Player _playerPrefab;

    // ���������� ��� ������ � ������� �������������� �������
    private PhotonView _photonView;

    // ������� �������
    private Location _location;

    // ����������� ������ � MonoBehaviour
    private void Awake()
    {
        // �������� ������ � �������� ������������� �������
        _photonView = GetComponent<PhotonView>();

        // ������� � ����� ������ ���� Location
        _location = FindObjectOfType<Location>();
    }
    // ���������� ��� ������� ����
    private void Start()
    {
        // ���� ��� ��� ������
        // �� ���� �� ����������� ���������� ������
        if (_photonView.IsMine)
        {
            // �������������� �������
            _location.Init();

            // �������� ����� SpawnPlayer()
            SpawnPlayer();
        }
    }
    private void SpawnPlayer()
    {
        // �������� ����� ��� ��������� ������
        // �� ������ ��� ����������� ������ � ����
        PlayerSpawnPoint spawnPoint = _location.SpawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1];

        // ������ ������� ������ ������ � �������� �����
        PhotonNetwork.Instantiate(_playerPrefab.name, spawnPoint.transform.position, Quaternion.identity);
    }
}