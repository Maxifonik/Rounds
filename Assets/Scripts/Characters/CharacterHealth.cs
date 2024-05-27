using System;
using UnityEngine;
using Photon.Pun;

public abstract class CharacterHealth : CharacterPart
{
    // Стартовое количество здоровья
    [SerializeField] private int _startHealthPoints = 100;

    // Очки здоровья персонажа
    private int _healthPoints;

    // Флаг смерти персонажа
    private bool _isDead;

    // Событие при смерти
    public Action OnDie;

    // Дополнительное событие при смерти
    // Со ссылкой на метод с параметром типа CharacterHealth
    public Action<CharacterHealth> OnDieWithObject;

    // Событие при изменении очков здоровья
    public Action OnAddHealthPoints;

    // Добавляем очки здоровья
    public void AddHealthPoints(int value)
    {
        // Вызываем метод RPCAddHealthPoints()
        // На всех подключённых клиентах
        PhotonView.RPC(nameof(RPCAddHealthPoints), RpcTarget.All, value);
    }
    // Получаем стартовое количество здоровья
    public int GetStartHealthPoints()
    {
        // Возвращаем стартовые очки здоровья
        return _startHealthPoints;
    }
    // Получаем текущее количество здоровья
    public int GetHealthPoints()
    {
        // Возвращаем текущие очки здоровья
        return _healthPoints;
    }
    // Инициализируем переменные
    protected override void OnInit()
    {
        // Задаём начальное значение здоровья
        _healthPoints = _startHealthPoints;

        // Ставим флаг в значение «живой»
        _isDead = false;
    }
    // Обрабатываем смерть персонажа
    private void Die()
    {
        // Ставим флаг в значение «мёртвый»
        _isDead = true;

        // Вызываем событие OnDie
        OnDie?.Invoke();

        // Вызываем событие OnDieWithObject
        // И передаём в него информацию о персонаже
        // То есть ссылку на объект типа CharacterHealth
        OnDieWithObject?.Invoke(this);
    }
    // Специальный атрибут
    // Для синхронизации действий игроков
    [PunRPC]

    protected void RPCAddHealthPoints(int value)
    {
        if (!PhotonView.IsMine || _isDead)
        {
            // Выходим из метода
            return;
        }
        _healthPoints += value;

        // Проверяем, что здоровье в пределах от нуля до заданного изначально
        Mathf.Clamp(_healthPoints, 0, _startHealthPoints);

        OnAddHealthPoints?.Invoke();

        // Если здоровье достигло нуля
        if (_healthPoints <= 0)
        {
            Die();
        }
    }
}