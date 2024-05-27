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
            // Создаём префаб элемента списка комнат
            // Добавляем его в контейнер
            RoomListElement element = Instantiate(_roomListElementPrefab, _roomListContainer);

            // Устанавливаем название комнаты
            element.SetRoomName(roomList[i].Name);

            // Обрабатываем событие OnJoinButtonClick
            // Вызываем метод JoinRoomButtonClick()
            element.OnJoinButtonClick += JoinRoomButtonClick;

            _roomListElements.Add(element);
        }
    }
    // Очищаем контейнер с комнатами
    private void ClearContainer()
    {
        // Проходим по списку комнат в обратном порядке
        for (int i = _roomListElements.Count - 1; i >= 0; i--)
        {
            // Удаляем каждую комнату
            Destroy(_roomListElements[i].gameObject);
        }
        _roomListElements.Clear();
    }
    // Вызывается при нажатии на кнопку входа в комнату
    private void JoinRoomButtonClick(string roomName)
    {
        // Вызываем событие OnJoinRoomButtonClick
        // Передаём в него комнату с заданным именем
        OnJoinRoomButtonClick?.Invoke(roomName);
    }
}
