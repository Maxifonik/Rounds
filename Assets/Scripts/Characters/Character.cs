using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private CharacterPart[] _parts;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        _parts = GetComponents<CharacterPart>();

        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].Init();
        }
        InitDeath();
    }
    private void InitDeath()
    {
        // Получаем компонент здоровья персонажа 
        var health = GetPart<CharacterHealth>();

        // Обрабатываем событие OnDie без цикла
        health.OnDie += Stop;
    }
    private void Stop()
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].Stop();
        }
    }
    private T GetPart<T>() where T : CharacterPart
    {
        for (int i = 0; i < _parts.Length; i++)
        {
            if (_parts[i] is T part)
            {
                return part;
            }
        }
        return null;
    }
}