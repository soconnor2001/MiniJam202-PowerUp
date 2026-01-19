using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterType
{
    public string Name;
    public List<MonsterVariant> Variants;
    public List<Transform> SpawnPoints;
}
