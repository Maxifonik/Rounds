using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensController : MonoBehaviour
{
    private BaseScreen[] _screens;

    // ���� ���������� �������
    private Stack<BaseScreen> _prevScreens = new Stack<BaseScreen>();

    private BaseScreen _currentScreen;

    public static bool HasCurrent => Current;

    public static ScreensController Current { get; private set; }

    public void Init()
    {
        // ������������� ������� �����
        Current = this;

        // �������� ��� ������ �� �������� ��������
        _screens = GetComponentsInChildren<BaseScreen>(true);

        HideAllScreens();
    }

    public T ShowScreen<T>(bool insertToPrev = true) where T : BaseScreen
    {
        if (_currentScreen)
        {
            _currentScreen.SetActive(false);

            // ���� ����� ���� insertToPrev
            // � ��� �� ����� ��������
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
        // ���������� ����� �� ����� ����������
        ShowScreen(_prevScreens.Pop());
    }

    private void HideAllScreens()
    {
        // �������� �� ���� �������
        for (int i = 0; i < Current._screens.Length; i++)
        {
            // �������� ������
            _screens[i].SetActive(false);
        }
    }

    private void ShowScreen(BaseScreen screen)
    {
        // �������� ������� �����
        _currentScreen.SetActive(false);

        // ������ ����� ������� �����
        _currentScreen = screen;

        // ���������� ���
        _currentScreen.SetActive(true);
    }
}
