using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensController : MonoBehaviour
{
    private BaseScreen[] _screens;

    // Стек предыдущих экранов
    private Stack<BaseScreen> _prevScreens = new Stack<BaseScreen>();

    private BaseScreen _currentScreen;

    public static bool HasCurrent => Current;

    public static ScreensController Current { get; private set; }

    public void Init()
    {
        // Устанавливаем текущий экран
        Current = this;

        // Получаем все экраны из дочерних объектов
        _screens = GetComponentsInChildren<BaseScreen>(true);

        HideAllScreens();
    }

    public T ShowScreen<T>(bool insertToPrev = true) where T : BaseScreen
    {
        if (_currentScreen)
        {
            _currentScreen.SetActive(false);

            // Если стоит флаг insertToPrev
            // И это не экран загрузки
            if (insertToPrev && !(_currentScreen is LoadingScreen))
            {
                _prevScreens.Push(_currentScreen);
            }
        }
        _currentScreen = GetScreen<T>();

        _currentScreen.SetActive(true);

        return _currentScreen as T;
    }

    public T GetScreen<T>() where T : BaseScreen
    {
        for (int i = 0; i < Current._screens.Length; i++)
        {
            if (_screens[i] is T targetScreen)
            {
                return targetScreen;
            }
        }
        return null;
    }
    public void ShowPrevScreen()
    {
        // Показываем экран из стека предыдущих
        ShowScreen(_prevScreens.Pop());
    }

    private void HideAllScreens()
    {
        // Проходим по всем экранам
        for (int i = 0; i < Current._screens.Length; i++)
        {
            // Скрываем каждый
            _screens[i].SetActive(false);
        }
    }

    private void ShowScreen(BaseScreen screen)
    {
        // Скрываем текущий экран
        _currentScreen.SetActive(false);

        // Ставим новый текущий экран
        _currentScreen = screen;

        // Показываем его
        _currentScreen.SetActive(true);
    }
}
