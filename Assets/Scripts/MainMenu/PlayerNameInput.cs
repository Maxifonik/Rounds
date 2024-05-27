using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    // ��������� � ������ ����� ������
    private const string PlayerNamePrefKey = "PlayerName";
    public void Init()
    {
        // �������� ��� ������ �� ����������
        string playerName = GetPlayerName();

        // ������� ��������� ��� ����� ������
        TMP_InputField inputField = GetComponentInChildren<TMP_InputField>();

        // ���� ��������� �������
        if (inputField != null)
        {
            // ������������� ��� ������ � ���� �����
            inputField.text = playerName;

            // ��������� ��������, ������� ����� �����������
            // ��� ��������� ������ (����� �����)
            inputField.onValueChanged.AddListener(SetPlayerName);
        }
        // �������� ����� SetPlayerName()
        // ������� � ���� ��� ������
        SetPlayerName(playerName);
    }
    private void SetPlayerName(string value)
    {
        // ���� ���������� �������� ������
        if (string.IsNullOrEmpty(value))
        {
            // ������� �� ������
            return;
        }
        // ����� ��������� ������ � Photon
        PhotonNetwork.NickName = value;

        // ������������� ��� ������ � ����������
        PlayerPrefs.SetString(PlayerNamePrefKey, value);

        // ��������� ��������� � ����������
        PlayerPrefs.Save();
    }

    private string GetPlayerName()
    {
        // ���������, ��������� �� ��� ��� ������
        if (PlayerPrefs.HasKey(PlayerNamePrefKey))
        {
            // ���������� ���������� ���
            return PlayerPrefs.GetString(PlayerNamePrefKey);
        }
        // ���������� ����� ��������� ��� � ���������� ���
        return $"�����{Random.Range(0, 10000):00000}";
    }
}
