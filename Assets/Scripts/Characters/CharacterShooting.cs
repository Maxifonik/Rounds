using UnityEngine;

// Сделали класс абстрактным
// Наследуем его от CharacterPart
public abstract class CharacterShooting : CharacterPart
{
    // Оружие персонажа
    private Weapon _weapon;

    // Защищённый абстрактный метод Shooting()
    protected abstract void Shooting();

    // Защищённый абстрактный метод Reloading()
    protected abstract void Reloading();

    // Заполняем ссылки на компоненты
    protected override void OnInit()
    {
        // Получаем Weapon из дочерних объектов
        _weapon = GetComponentInChildren<Weapon>();

        // Вызываем у оружия метод Init()
        _weapon.Init();
    }
    // Готовимся к стрельбе
    protected void Shoot()
    {
        // Вызываем у оружия метод Shoot()
        _weapon.Shoot();
    }
    // Проверяем, есть ли пули в магазине
    protected bool CheckHasBulletsInRow()
    {
        // Возвращаем результат проверки
        // После вызова одноимённого метода у оружия
        return _weapon.CheckHasBulletsInRow();
    }
    // Запускаем перезарядку оружия
    protected void Reload()
    {
        // Вызываем у оружия метод Reload()
        _weapon.Reload();
    }
    // Вызывается каждый кадр
    private void Update()
    {
        // Если персонаж не активен
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // Выходим из метода
            return;
        }
        // Вызываем метод Shooting()
        Shooting();

        // Вызываем метод Reloading()
        Reloading();
    }
}