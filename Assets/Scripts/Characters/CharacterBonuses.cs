using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public abstract class CharacterBonuses : CharacterPart
{
    [SerializeField] private List<BonusType> _existingBonusTypes;

    private List<BonusApplier> _bonusAppliers = new List<BonusApplier>()
{
    new ShootCountBonusApplier(),
};
    public void AddBonus(BonusType type)
    {
        _existingBonusTypes.Add(type);
    }
    protected override void OnInit()
    {
        // ���� � ������� ���� ��������� PhotonView
        // � �� ����������� �������� ������
        if (PhotonView && PhotonView.IsMine)
        {
            ApplyBonuses();

            // ������ ����������� ���-�������
            // ��� ����� ������� ���� �����-��������
            // ��� ������� ������� ������
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();

            // �������� �� ������ ��������� �������
            for (int i = 0; i < _existingBonusTypes.Count; i++)
            {
                // ��������� ��� ������� �����-��������
                // ����� ���� � ������ ���� BonusType{i}
                // � �������� � ������� ��� ������ �� ������
                hashtable.Add($"BonusType{i}", _existingBonusTypes[i]);
            }
            // ������������� ���-�������
            // ��� ���������������� �������� ���������� ������
            // �������� ������� ���������� � ��������� �������
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }
    }
    // ����������, ����� �������� ������ ����������� �� ����
    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        // ���� � ������� ���� ��������� PhotonView
        // � �� �� ����������� �������� ������
        // � ��� �������� � ������� �����
        // �� ���� � ���� ���������� ��������
        if (PhotonView && !PhotonView.IsMine && targetPlayer == PhotonView.Owner)
        {
            // ������� ������ ��������� �������
            _existingBonusTypes.Clear();

            // �������� �� ��������� ���������
            for (int i = 0; i < changedProps.Count; i++)
            {
                // ��������� �� ������� �������
                // � ��������, ����������� �� ��� ������
                var element = changedProps[$"BonusType{i}"];

                // ���� ������� �� ������
                if (element != null)
                {
                    // ��������� ��� � ������ ��������� �������
                    // ������� ��� � ���� BonusType
                    _existingBonusTypes.Add((BonusType)element);
                }
            }
            ApplyBonuses();
        }
    }
    private void ApplyBonuses()
    {
        // ��������� � ������ ���������� ���� BonusApplier
        // �� �������� �������� �������� ������� ������� ���������
        _bonusAppliers.AddRange(GetComponentsInChildren<BonusApplier>());

        // �������� �� �������� ���������� �������
        for (int i = 0; i < _bonusAppliers.Count; i++)
        {
            // �������� � ������� ����� ApplyBonus()
            _bonusAppliers[i].ApplyBonus(_existingBonusTypes, gameObject);
        }
    }
}