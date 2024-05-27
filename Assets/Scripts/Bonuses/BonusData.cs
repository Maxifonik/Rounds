using System;
using UnityEngine;

[Serializable]

public class BonusData
{
    [SerializeField] private float _chance;

    [SerializeField] private BonusType _type;

    public float Chance => _chance;

    public BonusType Type => _type;
}