using UnityEngine;

public abstract class BulletHit
{
    protected GameObject HitPrefab;

    protected int FinalHitType;

    protected int Damage;

    protected float Impulse;

    public BulletHit(int finalHitType, GameObject hitPrefab)
    {
        FinalHitType = finalHitType;

        HitPrefab = hitPrefab;
    }
    public void Init(int damage, float impulse)
    {
        Damage = damage;

        Impulse = impulse;
    }
    public abstract void Hit(Collision collision, Transform bulletTransform);

    protected bool CheckCharacterHit(Collider collider)
    {
        CharacterHealth hitedHealth = collider.GetComponentInParent<CharacterHealth>();

        if (hitedHealth)
        {
            hitedHealth.AddHealthPoints(-Damage);
            return true;
        }
        return false;
    }
    protected bool CheckPhysicObjectHit(Collider collider, Vector3 direction, Vector3 point)
    {
        IPhysicHittable hittedPhysicObject = collider.GetComponentInParent<IPhysicHittable>();

        if (hittedPhysicObject != null)
        {
            hittedPhysicObject.Hit(direction * Impulse, point);
            return true;
        }
        return false;
    }
}