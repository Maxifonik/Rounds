using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    // Константа с ключом имени игрока
    private const string PlayerNamePrefKey = "PlayerName";
    public void Init()
    {
        // Получаем имя игрока из сохранений
        string playerName = GetPlayerName();

        // Находим компонент для ввода текста
        TMP_InputField inputField = GetComponentInChildren<TMP_InputField>();

        // Если компонент нашёлся
        if (inputField != null)
        {
            // Устанавливаем имя игрока в поле ввода
            inputField.text = playerName;

            // Добавляем действие, которое будет выполняться
            // При изменении текста (вводе имени)
            inputField.onValueChanged.AddListener(SetPlayerName);
        }
        // Вызываем метод SetPlayerName()
        // Передаём в него имя игрока
        SetPlayerName(playerName);
    }
    private void SetPlayerName(string value)
    {
        // Если переданное значение пустое
        if (string.IsNullOrEmpty(value))
        {
            // Выходим из метода
            return;
        }
        // Задаём псевдоним игрока в Photon
        PhotonNetwork.NickName = value;

        // Устанавливаем имя игрока в настройках
        PlayerPrefs.SetString(PlayerNamePrefKey, value);

        // Сохраняем изменения в настройках
        PlayerPrefs.Save();
    }

    private string GetPlayerName()
    {
        // Проверяем, сохранено ли уже имя игрока
        if (PlayerPrefs.HasKey(PlayerNamePrefKey))
        {
            // Возвращаем сохранённое имя
            return PlayerPrefs.GetString(PlayerNamePrefKey);
        }
        // Генерируем новое случайное имя и возвращаем его
        return $"Игрок{Random.Range(0, 10000):00000}";
    }
}
