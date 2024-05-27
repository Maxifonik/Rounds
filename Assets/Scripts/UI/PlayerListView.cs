using System.Collections.Generic;
using UnityEngine;

public class PlayerListView : MonoBehaviour
{
    // Префаб элемента списка игроков
    [SerializeField] private PlayerListElement _playerListElementPrefab;

    // Трансформа контейнера списка
    [SerializeField] private Transform _playerListContainer;

    // Список элементов с игроками
    private List<PlayerListElement> _playerListElements = new List<PlayerListElement>();
    public void SetPlayers(Photon.Realtime.Player[] newPlayers)
    {
        ClearContainer();

        for (int i = 0; i < newPlayers.Length; i++)
        {
            // Вызываем метод AddPlayer()
            // Передаём в него очередного игрока
            AddPlayer(newPlayers[i]);
        }
    }

    public void AddPlayer(Photon.Realtime.Player newPlayer)
    {
        // Создаём новый элемент интерфейса для заданного игрока
        PlayerListElement element = Instantiate(_playerListElementPrefab, _playerListContainer);

        // Ставим данные игрока в элемент интерфейса
        element.SetPlayer(newPlayer);

        _playerListElements.Add(element);
    }
    public void RemovePlayer(Photon.Realtime.Player otherPlayer)
    {
        // Задаём переменную для текущего элемента
        PlayerListElement element;

        // Проходим по списку элементов в обратном порядке
        for (int i = _playerListElements.Count - 1; i >= 0; i--)
        {
            // Получаем текущий элемент
            element = _playerListElements[i];

            // Если он соответствует удаляемому игроку
            if (element.CheckPlayer(otherPlayer))
            {
                // Удаляем элемент из списка
                _playerListElements.Remove(element);

                // Удаляем объект элемента из игры
                Destroy(element.gameObject);
            }
        }
    }

    public void ClearContainer()
    {
        // Проходим по списку элементов в обратном порядке
        for (int i = _playerListElements.Count - 1; i >= 0; i--)
        {
            // Удаляем объект элемента из игры
            Destroy(_playerListElements[i].gameObject);
        }
        // Очищаем список элементов
        _playerListElements.Clear();
    }

}
