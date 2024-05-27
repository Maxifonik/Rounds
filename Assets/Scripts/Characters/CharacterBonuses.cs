using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public abstract class CharacterBonuses : CharacterPart
{
    [SerializeField] private List<BonusType> _existingBonusTypes;

    private List<BonusApplier> _bonusAppliers = new List<BonusApplier>()
{
    new ShootCountBonusApplier(),
};
    public void AddBonus(BonusType type)
    {
        _existingBonusTypes.Add(type);
    }
    protected override void OnInit()
    {
        // Если у объекта есть компонент PhotonView
        // И он принадлежит текущему игроку
        if (PhotonView && PhotonView.IsMine)
        {
            ApplyBonuses();

            // Создаём специальную хеш-таблицу
            // Она будет хранить пары «ключ-значение»
            // Для сетевых свойств игрока
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();

            // Проходим по списку собранных бонусов
            for (int i = 0; i < _existingBonusTypes.Count; i++)
            {
                // Добавляем для каждого «ключ-значение»
                // Здесь ключ — строка типа BonusType{i}
                // А значение — текущий тип бонуса из списка
                hashtable.Add($"BonusType{i}", _existingBonusTypes[i]);
            }
            // Устанавливаем хеш-таблицу
            // Как пользовательские свойства локального игрока
            // Обновляя сетевую информацию о собранных бонусах
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
    }
    // Вызывается, когда свойства игрока обновляются по сети
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // Если у объекта есть компонент PhotonView
        // И он не принадлежит текущему игроку
        // И его владелец — целевой игрок
        // То есть у него обновились свойства
        if (PhotonView && !PhotonView.IsMine && targetPlayer == PhotonView.Owner)
        {
            // Очищаем список собранных бонусов
            _existingBonusTypes.Clear();

            // Проходим по изменённым свойствам
            for (int i = 0; i < changedProps.Count; i++)
            {
                // Извлекаем из каждого элемент
                // С индексом, указывающим на тип бонуса
                var element = changedProps[$"BonusType{i}"];

                // Если элемент не пустой
                if (element != null)
                {
                    // Добавляем его в список собранных бонусов
                    // Приводя его к типу BonusType
                    _existingBonusTypes.Add((BonusType)element);
                }
            }
            ApplyBonuses();
        }
    }
    private void ApplyBonuses()
    {
        // Добавляем в список компоненты типа BonusApplier
        // Из дочерних объектов текущего объекта бонусов персонажа
        _bonusAppliers.AddRange(GetComponentsInChildren<BonusApplier>());

        // Проходим по объектам применения бонусов
        for (int i = 0; i < _bonusAppliers.Count; i++)
        {
            // Вызываем у каждого метод ApplyBonus()
            _bonusAppliers[i].ApplyBonus(_existingBonusTypes, gameObject);
        }
    }
}