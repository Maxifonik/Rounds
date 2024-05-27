using Photon.Pun;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    // ��������� � ��������� ����� �������� ����
    public const string MainMenuSceneName = "MainMenuScene";

    // ��������� � ��������� ����� ����
    public const string GameSceneName = "GameScene";

    // ��������� ����� �������� ����
    public static void LoadMainMenu()
    {
        // ���������� SceneManager ��� ��������
        SceneManager.LoadScene(MainMenuSceneName);
    }
    // ��������� ����� ����
    public static void LoadGame()
    {
        // ���������� PhotonNetwork ��� ��������
        PhotonNetwork.LoadLevel(GameSceneName);
    }
}

