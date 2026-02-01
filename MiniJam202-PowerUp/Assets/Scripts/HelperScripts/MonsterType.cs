using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MonsterType
{
    public string Name;
    public float SpawnHeight;
    public List<MonsterVariant> Variants;
    public List<Transform> SpawnPoints;

    public float GetCurrentSpawnChance(float TimeSinceStart)
    {
        var totalChance = 0f;
        foreach (var variant in Variants)
        {
            totalChance += variant.GetCurrentSpawnChance(TimeSinceStart);
        }
        return totalChance;
    }
    public Transform GetRandomSpawnPoint(Transform PlayerTransform, float minDistFromPlayer=0)
    {
        Transform chosenSpawnPoint;
        var attemptsLeft = 5;
        do
        {
            chosenSpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)];
            attemptsLeft--;
        } while (attemptsLeft > 0 && minDistFromPlayer > Vector3.Distance(chosenSpawnPoint.position, PlayerTransform.position));
        if (attemptsLeft == 0)
        {
            chosenSpawnPoint = GetFarthestSpawnPoint(PlayerTransform);
        }

        return chosenSpawnPoint;

        
    }

    public Transform GetFarthestSpawnPoint(Transform Origin)
    {
        Transform farthestSpawnPoint = SpawnPoints[0];
        float maxDist = 0;
        foreach(var spawnPoint in SpawnPoints)
        {
            float dist = Vector3.Distance(Origin.position,spawnPoint.position);
            if(dist > maxDist)
            {
                farthestSpawnPoint = spawnPoint;
                maxDist = dist;
            }
        }
        return farthestSpawnPoint;
    }
}
