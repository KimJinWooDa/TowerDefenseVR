using UnityEngine;

[CreateAssetMenu(fileName = "MonsterInfo", menuName = "MonsterInfo", order = int.MinValue)]
public class MonsterInfo : ScriptableObject
{
    public float Damage;
    public float Hp;
    public float Speed;

    public int Gold;
}
