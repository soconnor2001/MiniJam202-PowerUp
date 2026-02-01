using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterVariant
{
    public GameObject Monster;
    public AnimationCurve SpawnCurve;

    public float GetCurrentSpawnChance(float timeSinceStart)
    {
        return SpawnCurve.Evaluate(timeSinceStart);
    }
}


