using UnityEngine;

// Сделали класс абстрактным
// Наследуем его от CharacterPart
public abstract class CharacterAiming : CharacterPart
{
    // Оружие персонажа
    private Weapon _weapon;

    // Свойство с данными о текущем оружии
    protected Weapon Weapon => _weapon;

    // Заполняем ссылки на компоненты
    protected override void OnInit()
    {
        // Получаем Weapon из дочерних объектов
        _weapon = GetComponentInChildren<Weapon>();
    }
}