using System;
using UnityEngine;

[Serializable]
public class AttackingStateConfig
{
    [field: SerializeField, Range(0, 100)] public int MeleeDamage { get; private set; }
    [field: SerializeField, Range(0, 10)] public int VampirismDuration { get; private set; }
    [field: SerializeField, Range(0, 5)] public int VampirismPower { get; private set; }
    [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }
}
