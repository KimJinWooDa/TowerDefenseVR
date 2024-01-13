using System;

[Serializable]
public class TowerState
{
    public string TowerLevel;
    public float Damage;
    public float AttackSpeed;
    public float NumberOfTargets;

    public int NextUpgradeCost;
}
