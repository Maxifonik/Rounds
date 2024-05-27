using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class RoomListElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _roomNameText;

    [SerializeField] private Button _joinButton;

    // Событие входа в комнату
    public Action<string> OnJoinButtonClick;

    private string _roomName;

    public void SetRoomName(string value)
    {
        _roomName = value;

        _roomNameText.text = value;
    }
    private void Start()
    {
        _joinButton.onClick.AddListener(JoinButtonClick);
    }
    private void JoinButtonClick()
    {
        OnJoinButtonClick?.Invoke(_roomName);
    }
}