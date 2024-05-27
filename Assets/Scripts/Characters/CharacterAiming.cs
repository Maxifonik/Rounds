using UnityEngine;

// ������� ����� �����������
// ��������� ��� �� CharacterPart
public abstract class CharacterAiming : CharacterPart
{
    // ������ ���������
    private Weapon _weapon;

    // �������� � ������� � ������� ������
    protected Weapon Weapon => _weapon;

    // ��������� ������ �� ����������
    protected override void OnInit()
    {
        // �������� Weapon �� �������� ��������
        _weapon = GetComponentInChildren<Weapon>();
    }
}