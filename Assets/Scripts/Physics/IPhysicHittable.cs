using UnityEngine;

// ���������� ��������� IPhysicHittable
public interface IPhysicHittable
{
    // ����� ������ ����� Hit()
    void Hit(Vector3 force, Vector3 position);
}