using UnityEngine;
using System;
using System.Collections.Generic;
using Photon.Realtime;

public class RoomListView : MonoBehaviour
{
    [SerializeField] private RoomListElement _roomListElementPrefab;

    [SerializeField] private Transform _roomListContainer;

    private List<RoomListElement> _roomListElements = new List<RoomListElement>();

    public Action<string> OnJoinRoomButtonClick;
    public void SetRoomList(List<RoomInfo> roomList)
    {
        ClearContainer();

        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            // ������ ������ �������� ������ ������
            // ��������� ��� � ���������
            RoomListElement element = Instantiate(_roomListElementPrefab, _roomListContainer);

            // ������������� �������� �������
            element.SetRoomName(roomList[i].Name);

            // ������������ ������� OnJoinButtonClick
            // �������� ����� JoinRoomButtonClick()
            element.OnJoinButtonClick += JoinRoomButtonClick;

            _roomListElements.Add(element);
        }
    }
    // ������� ��������� � ���������
    private void ClearContainer()
    {
        // �������� �� ������ ������ � �������� �������
        for (int i = _roomListElements.Count - 1; i >= 0; i--)
        {
            // ������� ������ �������
            Destroy(_roomListElements[i].gameObject);
        }
        _roomListElements.Clear();
    }
    // ���������� ��� ������� �� ������ ����� � �������
    private void JoinRoomButtonClick(string roomName)
    {
        // �������� ������� OnJoinRoomButtonClick
        // ������� � ���� ������� � �������� ������
        OnJoinRoomButtonClick?.Invoke(roomName);
    }
}
