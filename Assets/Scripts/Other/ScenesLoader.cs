using Photon.Pun;
using UnityEngine.SceneManagement;

public static class ScenesLoader
{
    // Константа с названием сцены главного меню
    public const string MainMenuSceneName = "MainMenuScene";

    // Константа с названием сцены игры
    public const string GameSceneName = "GameScene";

    // Загружаем сцену главного меню
    public static void LoadMainMenu()
    {
        // Используем SceneManager для загрузки
        SceneManager.LoadScene(MainMenuSceneName);
    }
    // Загружаем сцену игры
    public static void LoadGame()
    {
        // Используем PhotonNetwork для загрузки
        PhotonNetwork.LoadLevel(GameSceneName);
    }
}

