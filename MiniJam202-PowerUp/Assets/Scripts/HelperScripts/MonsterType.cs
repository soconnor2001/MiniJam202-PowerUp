using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterType
{
    public string Name;
    public float SpawnHeight;
    public List<MonsterVariant> Variants;
    public List<Transform> SpawnPoints;
}
