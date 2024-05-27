using UnityEngine;

// ������� ����� �����������
// ��������� ��� �� CharacterPart
public abstract class CharacterShooting : CharacterPart
{
    // ������ ���������
    private Weapon _weapon;

    // ���������� ����������� ����� Shooting()
    protected abstract void Shooting();

    // ���������� ����������� ����� Reloading()
    protected abstract void Reloading();

    // ��������� ������ �� ����������
    protected override void OnInit()
    {
        // �������� Weapon �� �������� ��������
        _weapon = GetComponentInChildren<Weapon>();

        // �������� � ������ ����� Init()
        _weapon.Init();
    }
    // ��������� � ��������
    protected void Shoot()
    {
        // �������� � ������ ����� Shoot()
        _weapon.Shoot();
    }
    // ���������, ���� �� ���� � ��������
    protected bool CheckHasBulletsInRow()
    {
        // ���������� ��������� ��������
        // ����� ������ ����������� ������ � ������
        return _weapon.CheckHasBulletsInRow();
    }
    // ��������� ����������� ������
    protected void Reload()
    {
        // �������� � ������ ����� Reload()
        _weapon.Reload();
    }
    // ���������� ������ ����
    private void Update()
    {
        // ���� �������� �� �������
        if (!PhotonView || !PhotonView.IsMine || !IsActive)
        {
            // ������� �� ������
            return;
        }
        // �������� ����� Shooting()
        Shooting();

        // �������� ����� Reloading()
        Reloading();
    }
}