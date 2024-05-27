using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI _roomNameText;

    [SerializeField] private Button _leaveRoomButton;

    [SerializeField] private Button _playButton;

    [SerializeField] private PlayerListView _playerListView;

    public PlayerListView PlayerListView => _playerListView;

    public Action OnPlayButtonClick;

    public Action OnLeaveButtonClick;

    public void SetRoomNameText(string value)
    {
        _roomNameText.text = value;
    }
    private void Start()
    {
        _leaveRoomButton.onClick.AddListener(LeaveButtonClick);

        _playButton.onClick.AddListener(PlayButtonClick);
    }
    private void LeaveButtonClick()
    {
        OnLeaveButtonClick?.Invoke();
    }
    // ����� ���������� ������ ������ ����
    public void SetActivePlayButton(bool value)
    {
        // ������ � ������ value
        _playButton.gameObject.SetActive(value);
    }
    private void PlayButtonClick()
    {
        // �������� ������� OnPlayButtonClick
        OnPlayButtonClick?.Invoke();
    }
}