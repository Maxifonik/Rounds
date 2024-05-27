using System;
using UnityEngine;
using Photon.Pun;

public abstract class CharacterHealth : CharacterPart
{
    // ��������� ���������� ��������
    [SerializeField] private int _startHealthPoints = 100;

    // ���� �������� ���������
    private int _healthPoints;

    // ���� ������ ���������
    private bool _isDead;

    // ������� ��� ������
    public Action OnDie;

    // �������������� ������� ��� ������
    // �� ������� �� ����� � ���������� ���� CharacterHealth
    public Action<CharacterHealth> OnDieWithObject;

    // ������� ��� ��������� ����� ��������
    public Action OnAddHealthPoints;

    // ��������� ���� ��������
    public void AddHealthPoints(int value)
    {
        // �������� ����� RPCAddHealthPoints()
        // �� ���� ������������ ��������
        PhotonView.RPC(nameof(RPCAddHealthPoints), RpcTarget.All, value);
    }
    // �������� ��������� ���������� ��������
    public int GetStartHealthPoints()
    {
        // ���������� ��������� ���� ��������
        return _startHealthPoints;
    }
    // �������� ������� ���������� ��������
    public int GetHealthPoints()
    {
        // ���������� ������� ���� ��������
        return _healthPoints;
    }
    // �������������� ����������
    protected override void OnInit()
    {
        // ����� ��������� �������� ��������
        _healthPoints = _startHealthPoints;

        // ������ ���� � �������� ������
        _isDead = false;
    }
    // ������������ ������ ���������
    private void Die()
    {
        // ������ ���� � �������� �������
        _isDead = true;

        // �������� ������� OnDie
        OnDie?.Invoke();

        // �������� ������� OnDieWithObject
        // � ������� � ���� ���������� � ���������
        // �� ���� ������ �� ������ ���� CharacterHealth
        OnDieWithObject?.Invoke(this);
    }
    // ����������� �������
    // ��� ������������� �������� �������
    [PunRPC]

    protected void RPCAddHealthPoints(int value)
    {
        if (!PhotonView.IsMine || _isDead)
        {
            // ������� �� ������
            return;
        }
        _healthPoints += value;

        // ���������, ��� �������� � �������� �� ���� �� ��������� ����������
        Mathf.Clamp(_healthPoints, 0, _startHealthPoints);

        OnAddHealthPoints?.Invoke();

        // ���� �������� �������� ����
        if (_healthPoints <= 0)
        {
            Die();
        }
    }
}