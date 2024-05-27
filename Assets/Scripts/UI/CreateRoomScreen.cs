using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CreateRoomScreen : BaseScreen
{
    // Поле ввода названия комнаты
    [SerializeField] private TMP_InputField _roomNameInputField;

    // Кнопка создания комнаты
    [SerializeField] private Button _createRoomButton;

    // Кнопка возврата назад
    [SerializeField] private Button _backButton;

    // Событие создания комнаты
    public Action<string> OnCreateRoomButtonClick;

    // Вызывается при запуске игры
    private void Start()
    {
        // Обрабатываем нажатие на кнопку создания комнаты
        _createRoomButton.onClick.AddListener(CreateRoomButtonClick);

        // Обрабатываем нажатие на кнопку возврата назад
        _backButton.onClick.AddListener(BackButtonClick);
    }
    // Вызывается при нажатии на кнопку создания комнаты
    private void CreateRoomButtonClick()
    {
        // Если поле ввода названия комнаты пустое
        if (string.IsNullOrEmpty(_roomNameInputField.text))
        {
            // Выводим сообщение об ошибке
            _roomNameInputField.text = "Добавьте название комнаты";

            return;
        }
        // Вызываем событие OnCreateRoomButtonClick
        // Передаём в него название комнаты
        OnCreateRoomButtonClick?.Invoke(_roomNameInputField.text);
    }
    // Вызывается при нажатии на кнопку возврата назад
    private void BackButtonClick()
    {
        ScreensController.Current.ShowPrevScreen();
    }
}