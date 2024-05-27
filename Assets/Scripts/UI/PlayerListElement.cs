using TMPro;
using UnityEngine;

public class PlayerListElement : MonoBehaviour
{
    // Надпись с именем игрока
    [SerializeField] private TextMeshProUGUI _playerNameText;

    // Информация об игроке от Photon
    private Photon.Realtime.Player _player;

    // Задаём данные игрока
    public void SetPlayer(Photon.Realtime.Player value)
    {
        // Делаем информацию об игроке равной value
        _player = value;

        // Делаем имя игрока равным value.NickName
        _playerNameText.text = value.NickName;
    }
    // Проверяем данные игрока
    public bool CheckPlayer(Photon.Realtime.Player player)
    {
        // Возвращаем true, если заданный игрок равен _player
        // Иначе возвращаем false
        return player == _player;
    }
}