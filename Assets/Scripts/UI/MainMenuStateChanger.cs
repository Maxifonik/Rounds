using UnityEngine;

using Photon.Pun;

using Photon.Realtime;

using System.Collections.Generic;
public class MainMenuStateChanger : MonoBehaviourPunCallbacks
{
    // ������ ����
    [SerializeField] private string _gameVersion = "1";

    // ������������ ����� ������� � �������
    [SerializeField] private int _maxPlayersPerRoom = 2;

    // ���������� �������
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
        // ���������� ����� �����
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
        // ���������� ����� ������
        ErrorScreen errorScreen = _screenController.ShowScreen<ErrorScreen>();

        // ������������� ����� ������
        errorScreen.SetErrorText($"��� ������: {returnCode}; ���������: {message}");
    }

    public override void OnLeftRoom()
    {
        _screenController.ShowPrevScreen();

        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        roomScreen.PlayerListView.ClearContainer();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ����������� ����� �����
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // ������� �� ���� ����� ������ ������
        lobbyScreen.RoomListView.SetRoomList(roomList);
    }
    private void Start()
    {
        // �������� ����� Init()
        Init();
    }

    private void Init()
    {
        // ������ �������������� ������������� ����� ��� ���� �������
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� ���������� �������
        _screenController = FindAnyObjectByType<ScreensController>();

        // �������� � ���� ����� Init()
        _screenController.Init();

        // �������� ����� Connect()
        Connect();

        // ����������� ����� �����
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // ������������ ������� OnJoinRoomButtonClick
        // �������� ����� JoinRoom()
        lobbyScreen.RoomListView.OnJoinRoomButtonClick += JoinRoom;

        // ����������� ����� �������� �������
        CreateRoomScreen createRoomScreen = _screenController.GetScreen<CreateRoomScreen>();

        // ������������ ������� OnCreateRoomButtonClick
        // �������� ����� CreateRoom()
        createRoomScreen.OnCreateRoomButtonClick += CreateRoom;

        // ����������� ����� �������
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // ������������ ������� OnLeaveButtonClick
        // �������� ����� LeaveRoom()
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
        // ������ � �����
        PhotonNetwork.JoinLobby();

        // ���������� ����� ��������
        _screenController.ShowScreen<LoadingScreen>();
    }
    private void CreateRoom(string name)
    {
        // ������ Photon ������� ����� �������
        // � ����������� ������ � �����������
        // ��� ����������� ������������ ����� �������
        PhotonNetwork.CreateRoom(name, new RoomOptions() { MaxPlayers = _maxPlayersPerRoom });

        // ���������� ����� ��������
        _screenController.ShowScreen<LoadingScreen>();
    }

    private void JoinRoom(string name)
    {
        // ������ Photon ���������� ��� � �������
        // � ����������� ������
        PhotonNetwork.JoinRoom(name);

        // ���������� ����� ��������
        _screenController.ShowScreen<LoadingScreen>();
    }

    private void LeaveRoom()
    {
        // ������ Photon ��������� ��� �� �������
        PhotonNetwork.LeaveRoom();

        // �� ���������� ����� �������� (����������� ��� false)
        _screenController.ShowScreen<LoadingScreen>(false);
    }
    private void OnDestroy()
    {
        // ���� ����������� ������� ���
        if (!_screenController)
        {
            // ������� �� ������
            return;
        }
        // ����������� ����� �������� �������
        CreateRoomScreen createRoomScreen = _screenController.GetScreen<CreateRoomScreen>();

        // ���� ���� ����� �������� �������
        if (createRoomScreen)
        {
            // ������������ �� ������� OnCreateRoomButtonClick
            createRoomScreen.OnCreateRoomButtonClick -= CreateRoom;
        }
        // ����������� ����� �����
        LobbyScreen lobbyScreen = _screenController.GetScreen<LobbyScreen>();

        // ���� ���� ����� ����� �� ������� ������
        if (lobbyScreen && lobbyScreen.RoomListView)
        {
            // ������������ �� ������� OnJoinRoomButtonClick
            lobbyScreen.RoomListView.OnJoinRoomButtonClick -= JoinRoom;
        }
        // ����������� ����� �������
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        if (roomScreen)
        {
            roomScreen.OnLeaveButtonClick -= LeaveRoom;

            roomScreen.OnPlayButtonClick -= ScenesLoader.LoadGame;
        }
    }
    // ���������� ��� ����� ������ � �������
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // �������� ���� �������
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // ��������� ������
        roomScreen.PlayerListView.AddPlayer(newPlayer);

        // �������� ����� RefreshPlayButton()
        RefreshPlayButton();
    }
    // ���������� ��� ������ ������ �� �������
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        // �������� ���� �������
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // ������� ������
        roomScreen.PlayerListView.RemovePlayer(otherPlayer);

        // �������� ����� RefreshPlayButton()
        RefreshPlayButton();
    }
    // ���������� ��� ������� ����� ������ (������-�������)
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        // �������� ����� RefreshPlayButton()
        RefreshPlayButton();
    }
    // ��������� ������ ������� ����
    private void RefreshPlayButton()
    {
        // �������� ���� �������
        RoomScreen roomScreen = _screenController.GetScreen<RoomScreen>();

        // ������ ������� ���� ������� ������ �� ������-�������
        // � ����� ������� �� ������
        bool isActive = PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
        roomScreen.SetActivePlayButton(isActive);
    }


}