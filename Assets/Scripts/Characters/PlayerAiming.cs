using UnityEngine;

// ��������� PlayerAiming �� CharacterAiming
public class PlayerAiming : CharacterAiming
{
    // ������� ������
    private Camera _mainCamera;

    // �������������� ����������
    protected override void OnInit()
    {
        // �������������� ������ �������� ������
        // �� ���� CharacterAiming
        base.OnInit();

        // ����������� _mainCamera ������ ������
        _mainCamera = Camera.main;
    }
    // ���������� ������ ����
    private void Update()
    {
        // ���� ����� �� �������
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // ������� �� ������
            return;
        }
        // �������� ����� Aiming()
        Aiming();
    }
    // ��������� ������������� ������
    private void Aiming()
    {
        // ��������� ������� � ���������� Z
        // ����� ���������� � �������
        float characterZDelta = transform.position.z - _mainCamera.transform.position.z;

        // ����������� ������� ������� ����
        // �� �������� ��������� � �������
        Vector3 mouseInWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * characterZDelta);

        // ������������ ������ � ������� ������� 
        Weapon.transform.LookAt(mouseInWorldPosition);
    }
}