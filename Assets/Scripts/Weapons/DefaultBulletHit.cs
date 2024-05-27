using UnityEngine;

public class DefaultBulletHit : BulletHit
{
    public DefaultBulletHit(int finalHitType, GameObject hitPrefab) : base(finalHitType, hitPrefab) { }

    public override void Hit(Collision collision, Transform bulletTransform)
    {
        bool isCharacterHit = CheckCharacterHit(collision.collider);

        CheckPhysicObjectHit(collision.collider, bulletTransform.forward, collision.contacts[0].point);

        if (!isCharacterHit)
        {
            Quaternion hitRotation = Quaternion.LookRotation(-bulletTransform.up, -bulletTransform.forward);

            GameObject hitSample = GameObject.Instantiate(HitPrefab, collision.contacts[0].point, hitRotation);
        }
    }
}