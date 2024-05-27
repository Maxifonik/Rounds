using UnityEngine.UI;

using UnityEngine;

public class LobbyScreen : BaseScreen
{
    [SerializeField] private PlayerNameInput _playerNameInput;

    [SerializeField] private RoomListView _roomListView;

    [SerializeField] private Button _createRoomButton;

    public RoomListView RoomListView => _roomListView;

    private void Start()
    {
        _playerNameInput.Init();

        _createRoomButton.onClick.AddListener(CreateRoomButtonClick);
    }
    private void CreateRoomButtonClick()
    {
        ScreensController.Current.ShowScreen<CreateRoomScreen>();
    }
}