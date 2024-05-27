using UnityEngine;

using Photon.Pun;

using Photon.Realtime;

using System.Collections.Generic;
public class MainMenuStateChanger : MonoBehaviourPunCallbacks
{
    // Версия игры
    [SerializeField] private string _gameVersion = "1";

    // Максимальное число игроков в комнате
    [SerializeField] private int _maxPlayersPerRoom = 2;

    // Контроллер экранов
    ScreensController _screenController;

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Connect();
    }

    public override void OnJoinedLobby()
    {
        // Отображаем экран лобби
        _screenController.ShowScreen<LobbyScreen>();
    }
    public override void OnJoinedRoom()
    {
        RoomScreen roomScreen = _screenController.ShowScreen<RoomScreen>();
        roomScreen.SetRoomNameText(PhotonNetwork.CurrentRoom.Name);

        roomScreen.PlayerListView.SetPlayers(PhotonNetwork.PlayerList);

        RefreshPlayButton();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // Отображаем экран ошибки
        ErrorScreen errorScreen = _screenController.ShowScreen<ErrorScreen>();

        // Устанавливаем текст ошибки
        errorScreen.SetErrorText($"Код ошибки: {returnCode}; сообщение: {message}");
    }

    public override void OnLeftRoom()
    {
        _screenController.ShowPrevScreen();

        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        roomScreen.PlayerListView.ClearContainer();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Запрашиваем экран лобби
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // Передаём на этот экран список комнат
        lobbyScreen.RoomListView.SetRoomList(roomList);
    }
    private void Start()
    {
        // Вызываем метод Init()
        Init();
    }

    private void Init()
    {
        // Ставим автоматическую синхронизацию сцены для всех игроков
        PhotonNetwork.AutomaticallySyncScene = true;

        // Ищем контроллер экранов
        _screenController = FindAnyObjectByType<ScreensController>();

        // Вызываем у него метод Init()
        _screenController.Init();

        // Вызываем метод Connect()
        Connect();

        // Запрашиваем экран лобби
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // Обрабатываем событие OnJoinRoomButtonClick
        // Вызываем метод JoinRoom()
        lobbyScreen.RoomListView.OnJoinRoomButtonClick += JoinRoom;

        // Запрашиваем экран создания комнаты
        CreateRoomScreen createRoomScreen = _screenController.GetScreen<CreateRoomScreen>();

        // Обрабатываем событие OnCreateRoomButtonClick
        // Вызываем метод CreateRoom()
        createRoomScreen.OnCreateRoomButtonClick += CreateRoom;

        // Запрашиваем экран комнаты
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // Обрабатываем событие OnLeaveButtonClick
        // Вызываем метод LeaveRoom()
        roomScreen.OnLeaveButtonClick += LeaveRoom;

        roomScreen.OnPlayButtonClick += ScenesLoader.LoadGame;
    }
    private void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            JoinLobby();
        }
        else
        {
            _screenController.ShowScreen<LoadingScreen>();

            PhotonNetwork.ConnectUsingSettings();

            PhotonNetwork.GameVersion = _gameVersion;
        }
    }

    private void JoinLobby()
    {
        // Входим в лобби
        PhotonNetwork.JoinLobby();

        // Отображаем экран загрузки
        _screenController.ShowScreen<LoadingScreen>();
    }
    private void CreateRoom(string name)
    {
        // Просим Photon создать новую комнату
        // С определённым именем и настройками
        // Где указывается максимальное число игроков
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = _maxPlayersPerRoom });

        // Отображаем экран загрузки
        _screenController.ShowScreen<LoadingScreen>();
    }

    private void JoinRoom(string name)
    {
        // Просим Photon подключить нас к комнате
        // С определённым именем
        PhotonNetwork.JoinRoom(name);

        // Отображаем экран загрузки
        _screenController.ShowScreen<LoadingScreen>();
    }

    private void LeaveRoom()
    {
        // Просим Photon отключить нас от комнаты
        PhotonNetwork.LeaveRoom();

        // Не отображаем экран загрузки (присваиваем ему false)
        _screenController.ShowScreen<LoadingScreen>(false);
    }
    private void OnDestroy()
    {
        // Если контроллера экранов нет
        if (!_screenController)
        {
            // Выходим из метода
            return;
        }
        // Запрашиваем экран создания комнаты
        CreateRoomScreen createRoomScreen = _screenController.GetScreen<CreateRoomScreen>();

        // Если есть экран создания комнаты
        if (createRoomScreen)
        {
            // Отписываемся от события OnCreateRoomButtonClick
            createRoomScreen.OnCreateRoomButtonClick -= CreateRoom;
        }
        // Запрашиваем экран лобби
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // Если есть экран лобби со списком комнат
        if (lobbyScreen && lobbyScreen.RoomListView)
        {
            // Отписываемся от события OnJoinRoomButtonClick
            lobbyScreen.RoomListView.OnJoinRoomButtonClick -= JoinRoom;
        }
        // Запрашиваем экран комнаты
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        if (roomScreen)
        {
            roomScreen.OnLeaveButtonClick -= LeaveRoom;

            roomScreen.OnPlayButtonClick -= ScenesLoader.LoadGame;
        }
    }
    // Вызывается при входе игрока в комнату
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // Получаем окно комнаты
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // Добавляем игрока
        roomScreen.PlayerListView.AddPlayer(newPlayer);

        // Вызываем метод RefreshPlayButton()
        RefreshPlayButton();
    }
    // Вызывается при выходе игрока из комнаты
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        // Получаем окно комнаты
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // Удаляем игрока
        roomScreen.PlayerListView.RemovePlayer(otherPlayer);

        // Вызываем метод RefreshPlayButton()
        RefreshPlayButton();
    }
    // Вызывается при сетевой смене игрока (мастер-клиента)
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        // Вызываем метод RefreshPlayButton()
        RefreshPlayButton();
    }
    // Обновляем кнопку «Начать игру»
    private void RefreshPlayButton()
    {
        // Получаем окно комнаты
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // Кнопка «Начать игру» активна только на мастер-клиенте
        // И когда комната не пустая
        bool isActive = PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
        roomScreen.SetActivePlayButton(isActive);
    }


}