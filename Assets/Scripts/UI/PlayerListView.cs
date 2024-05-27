using System.Collections.Generic;
using UnityEngine;

public class PlayerListView : MonoBehaviour
{
    // ������ �������� ������ �������
    [SerializeField] private PlayerListElement _playerListElementPrefab;

    // ���������� ���������� ������
    [SerializeField] private Transform _playerListContainer;

    // ������ ��������� � ��������
    private List<PlayerListElement> _playerListElements = new List<PlayerListElement>();
    public void SetPlayers(Photon.Realtime.Player[] newPlayers)
    {
        ClearContainer();

        for (int i = 0; i < newPlayers.Length; i++)
        {
            // �������� ����� AddPlayer()
            // ������� � ���� ���������� ������
            AddPlayer(newPlayers[i]);
        }
    }

    public void AddPlayer(Photon.Realtime.Player newPlayer)
    {
        // ������ ����� ������� ���������� ��� ��������� ������
        PlayerListElement element = Instantiate(_playerListElementPrefab, _playerListContainer);

        // ������ ������ ������ � ������� ����������
        element.SetPlayer(newPlayer);

        _playerListElements.Add(element);
    }
    public void RemovePlayer(Photon.Realtime.Player otherPlayer)
    {
        // ����� ���������� ��� �������� ��������
        PlayerListElement element;

        // �������� �� ������ ��������� � �������� �������
        for (int i = _playerListElements.Count - 1; i >= 0; i--)
        {
            // �������� ������� �������
            element = _playerListElements[i];

            // ���� �� ������������� ���������� ������
            if (element.CheckPlayer(otherPlayer))
            {
                // ������� ������� �� ������
                _playerListElements.Remove(element);

                // ������� ������ �������� �� ����
                Destroy(element.gameObject);
            }
        }
    }

    public void ClearContainer()
    {
        // �������� �� ������ ��������� � �������� �������
        for (int i = _playerListElements.Count - 1; i >= 0; i--)
        {
            // ������� ������ �������� �� ����
            Destroy(_playerListElements[i].gameObject);
        }
        // ������� ������ ���������
        _playerListElements.Clear();
    }

}
