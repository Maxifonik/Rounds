using System.Collections.Generic;
using UnityEngine;

public class BonusDataAccesser : MonoBehaviour
{
    [SerializeField] private BonusData[] _data;

    // Получаем случайный бонус
    // Проверив, какие бонусы игрок уже собрал
    public BonusData GetRandomBonus(List<BonusType> existingBonusTypes)
    {
        // Получаем возможные бонусы для выбора
        List<BonusData> possibleData = GetPossibleData(existingBonusTypes);

        // Суммируем шансы всех возможных бонусов
        float sumChance = GetSumChance(possibleData);

        float rand = Random.Range(0, sumChance);

        for (int i = 0; i < possibleData.Count; i++)
        {
            // Если случайное число <= шансу бонуса
            if (rand <= possibleData[i].Chance)
            {
                // Возвращаем этот бонус
                return possibleData[i];
            }
            // Уменьшаем случайное число на шанс бонуса
            rand -= possibleData[i].Chance;
        }
        return null;
    }
    private List<BonusData> GetPossibleData(List<BonusType> existingBonusTypes)
    {
        // Создаём новый список ещё не активированных бонусов
        List<BonusData> possibleData = new List<BonusData>();

        for (int i = 0; i < _data.Length; i++)
        {
            if (!existingBonusTypes.Contains(_data[i].Type))
            {
                possibleData.Add(_data[i]);
            }
        }
        return possibleData;
    }
    private float GetSumChance(List<BonusData> data)
    {
        float sumChance = 0;

        for (int i = 0; i < data.Count; i++)
        {
            sumChance += data[i].Chance;
        }
        return sumChance;
    }
}