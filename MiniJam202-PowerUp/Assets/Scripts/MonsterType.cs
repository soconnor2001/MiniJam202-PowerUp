using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterType
{
    public string Monster;
    public List<MonsterVariant> Variants;
    public List<GameObject> SpawnPoints;
}
