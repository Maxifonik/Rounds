using UnityEngine;

// Наследуем PlayerShooting от CharacterShooting
public class PlayerShooting : CharacterShooting
{
    // Флаг автоматической перезарядки
    [SerializeField] protected bool _autoReloading = true;

    // Заполняем ссылки на компоненты
    protected override void OnInit()
    {
        // Инициализируем объект базового класса
        // То есть CharacterShooting
        base.OnInit();
    }
    // Начинаем стрелять
    protected override void Shooting()
    {
        // Если нажата левая кнопка мыши
        if (Input.GetMouseButton(0))
        {
            // Вызываем метод Shoot()
            Shoot();

            // Вызываем метод AutoReloading()
            AutoReloading();
        }
    }
    // Перезаряжаем оружие
    protected override void Reloading()
    {
        // Если нет пуль и нажата левая кнопка мыши
        // Или нажата клавиша R
        if ((!CheckHasBulletsInRow() && Input.GetMouseButton(0)) || Input.GetKeyDown(KeyCode.R))
        {
            // Вызываем метод Reload()
            Reload();
        }
    }
    // Происходит перезарядка
    private void AutoReloading()
    {
        // Если перезарядка не нужна
        if (!_autoReloading)
        {
            // Выходим из метода
            return;
        }
        // Если закончились пули
        if (!CheckHasBulletsInRow())
        {
            // Вызываем метод Reload()
            Reload();
        }
    }
}