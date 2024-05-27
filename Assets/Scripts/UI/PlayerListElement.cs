using TMPro;
using UnityEngine;

public class PlayerListElement : MonoBehaviour
{
    // ������� � ������ ������
    [SerializeField] private TextMeshProUGUI _playerNameText;

    // ���������� �� ������ �� Photon
    private Photon.Realtime.Player _player;

    // ����� ������ ������
    public void SetPlayer(Photon.Realtime.Player value)
    {
        // ������ ���������� �� ������ ������ value
        _player = value;

        // ������ ��� ������ ������ value.NickName
        _playerNameText.text = value.NickName;
    }
    // ��������� ������ ������
    public bool CheckPlayer(Photon.Realtime.Player player)
    {
        // ���������� true, ���� �������� ����� ����� _player
        // ����� ���������� false
        return player == _player;
    }
}