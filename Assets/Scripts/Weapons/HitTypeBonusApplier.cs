using System.Collections.Generic;
using UnityEngine;

public class HitTypeBonusApplier : MonoBehaviour, BonusApplier
{
    [SerializeField] private GameObject _defaultHitPrefab;

    [SerializeField] private GameObject _explosionHitPrefab;

    public void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root)
    {

        BulletHit hit = GetHit(existingBonusTypes);

        Apply(hit, root);
    }

    private BulletHit GetHit(List<BonusType> existingBonusTypes)
    {
        int finalHitType = 0;

        for (int i = 0; i < existingBonusTypes.Count; i++)
        {
            int hitType = 0;

            // Выбираем действие в зависимости от типа бонуса
            switch (existingBonusTypes[i])
            {
                // Если бонус — слабый взрыв
                case BonusType.SmallExplosionHit:

                    hitType = 1;

                    break;

                // Если бонус — средний взрыв
                case BonusType.MediumExplosionHit:

                    hitType = 2;

                    break;

                // Если бонус — сильный взрыв
                case BonusType.LargeExplosionHit:

                    hitType = 3;

                    break;
            }

            // Если начальная силы взрыва < текущей
            if (finalHitType < hitType)
            {
                finalHitType = hitType;
            }
        }
        BulletHit hit = null;

        // Если взрывной бонус отсутствует
        if (finalHitType == 0)
        {
            // Создаём обычное попадание
            hit = new DefaultBulletHit(finalHitType, _defaultHitPrefab);
        }
        else
        {
            // Создаём попадание со взрывом
            hit = new ExplosionBulletHit(finalHitType, _explosionHitPrefab);
        }
        return hit;
    }
    private void Apply(BulletHit hit, GameObject root)
    {
        IHitTypeBonusDependent[] dependents = root.GetComponentsInChildren<IHitTypeBonusDependent>();

        for (int i = 0; i < dependents.Length; i++)
        {
            dependents[i].SetHit(hit);
        }
    }
}