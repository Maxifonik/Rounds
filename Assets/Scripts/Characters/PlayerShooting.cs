using UnityEngine;

// ��������� PlayerShooting �� CharacterShooting
public class PlayerShooting : CharacterShooting
{
    // ���� �������������� �����������
    [SerializeField] protected bool _autoReloading = true;

    // ��������� ������ �� ����������
    protected override void OnInit()
    {
        // �������������� ������ �������� ������
        // �� ���� CharacterShooting
        base.OnInit();
    }
    // �������� ��������
    protected override void Shooting()
    {
        // ���� ������ ����� ������ ����
        if (Input.GetMouseButton(0))
        {
            // �������� ����� Shoot()
            Shoot();

            // �������� ����� AutoReloading()
            AutoReloading();
        }
    }
    // ������������ ������
    protected override void Reloading()
    {
        // ���� ��� ���� � ������ ����� ������ ����
        // ��� ������ ������� R
        if ((!CheckHasBulletsInRow() && Input.GetMouseButton(0)) || Input.GetKeyDown(KeyCode.R))
        {
            // �������� ����� Reload()
            Reload();
        }
    }
    // ���������� �����������
    private void AutoReloading()
    {
        // ���� ����������� �� �����
        if (!_autoReloading)
        {
            // ������� �� ������
            return;
        }
        // ���� ����������� ����
        if (!CheckHasBulletsInRow())
        {
            // �������� ����� Reload()
            Reload();
        }
    }
}