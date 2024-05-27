using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CreateRoomScreen : BaseScreen
{
    // ���� ����� �������� �������
    [SerializeField] private TMP_InputField _roomNameInputField;

    // ������ �������� �������
    [SerializeField] private Button _createRoomButton;

    // ������ �������� �����
    [SerializeField] private Button _backButton;

    // ������� �������� �������
    public Action<string> OnCreateRoomButtonClick;

    // ���������� ��� ������� ����
    private void Start()
    {
        // ������������ ������� �� ������ �������� �������
        _createRoomButton.onClick.AddListener(CreateRoomButtonClick);

        // ������������ ������� �� ������ �������� �����
        _backButton.onClick.AddListener(BackButtonClick);
    }
    // ���������� ��� ������� �� ������ �������� �������
    private void CreateRoomButtonClick()
    {
        // ���� ���� ����� �������� ������� ������
        if (string.IsNullOrEmpty(_roomNameInputField.text))
        {
            // ������� ��������� �� ������
            _roomNameInputField.text = "�������� �������� �������";

            return;
        }
        // �������� ������� OnCreateRoomButtonClick
        // ������� � ���� �������� �������
        OnCreateRoomButtonClick?.Invoke(_roomNameInputField.text);
    }
    // ���������� ��� ������� �� ������ �������� �����
    private void BackButtonClick()
    {
        ScreensController.Current.ShowPrevScreen();
    }
}