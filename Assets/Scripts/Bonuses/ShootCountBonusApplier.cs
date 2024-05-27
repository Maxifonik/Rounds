using System.Collections.Generic;
using UnityEngine;

public class ShootCountBonusApplier : BonusApplier
{

    public void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root)
    {
        int shootCount = GetShootCount(existingBonusTypes);

        Apply(shootCount, root);

    }
    private int GetShootCount(List<BonusType> existingBonusTypes)
    {
        int finalShootCount = 1;
        for (int i = 0; i < existingBonusTypes.Count; i++)
        {
            int shootCount = 1;
            switch (existingBonusTypes[i])
            {
                case BonusType.DoubleShoot:
                    shootCount = 2;
                    break;
                case BonusType.TripleShoot:
                    shootCount = 3;
                    break;
                case BonusType.QuadrupleShoot:
                    shootCount = 4;
                    break;
            }

            if (finalShootCount < shootCount)
            {
                finalShootCount = shootCount;
            }
        }
        return finalShootCount;
    }
    private void Apply(int shootCount, GameObject root)
    {
        // Ищем все компоненты в дочерних объектах root
        // Которые реализуют интерфейс IShootCountBonusDependent
        IShootCountBonusDependent[] dependents = root.GetComponentsInChildren<IShootCountBonusDependent>();

        for (int i = 0; i < dependents.Length; i++)
        {
            dependents[i].SetShootCount(shootCount);
        }
    }
}