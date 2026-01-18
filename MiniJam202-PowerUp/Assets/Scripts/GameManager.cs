using UnityEngine;

using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{

    public List<MonsterType> Monsters;
    public float MinutesToMaxDifficulty;

    public float AverageSpawnRate;
    public float SpawnRateVariability;
    public int MaxMonsterToSpawn = 4;
    float TimeToNextSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimeToNextSpawn();

    }
    private void OnValidate()
    {
        if (AverageSpawnRate < SpawnRateVariability / 2)
        {
            SpawnRateVariability = 2*AverageSpawnRate;
        }
    }
    
    void ResetTimeToNextSpawn()
    {
        TimeToNextSpawn = ((UnityEngine.Random.value * SpawnRateVariability) / 2f);
        TimeToNextSpawn = (TimeToNextSpawn - (TimeToNextSpawn / 2)) + AverageSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        TimeToNextSpawn -= Time.deltaTime;
        if (TimeToNextSpawn <= 0)
        {
            SpawnMonsters();
            ResetTimeToNextSpawn();
        }
    }

    void SpawnMonsters()
    {
        (List<(GameObject, float)>, float) TypesList = GetMonsterTypeToSpawn();
        List<(GameObject, float)> weightedTypes = TypesList.Item1;
        float typeTotal = TypesList.Item2;

        int numToSpawn = UnityEngine.Random.Range(0, MaxMonsterToSpawn+1);
        List<GameObject> monsterToSpawn = new();
        for (int i = 0; i < numToSpawn; i++)
        {
            monsterToSpawn.Add(GetMonsterVariantToSpawn(weightedTypes, typeTotal));
        }

        //Pick an unweighted spawnpoint and Instantiate allmonster there, but not on top of each other (hopefully)

    }
    GameObject GetMonsterVariantToSpawn(List<(GameObject, float)> rates,float total)
    {
        float randVal = UnityEngine.Random.value * total;
        int index = 0;
        GameObject TypeToSpawn = default!;

        while (index < rates.Count && randVal > 0)
        {
            randVal = randVal - rates[index].Item2;
            if (randVal <= 0)
            {
                TypeToSpawn = rates[index].Item1;
            }
        }
        if (randVal > 0)
        {
            throw new ArithmeticException("GetMonsterTypeToSpawn() did not find a monster to Spawn");
        }

        return TypeToSpawn;
    }
    private (List<(GameObject, float)>,float) GetMonsterTypeToSpawn()
    {
        var chances = GetSpawnChanceDictionary();
        List<(List<(GameObject, float)>, float)> rates = chances.Item1;
        float total = chances.Item2;

        float randVal = UnityEngine.Random.value * total;
        int index = 0;
        (List<(GameObject, float)>,float) TypeToSpawn = default!;
        
        while (index < rates.Count && randVal > 0)
        {
            randVal = randVal - rates[index].Item2;
            if (randVal <= 0)
            {
                TypeToSpawn = rates[index];
            }
        }
        if (randVal > 0)
        {
            throw new ArithmeticException("GetMonsterTypeToSpawn() did not find a monster to Spawn");
        }

        return TypeToSpawn;
    }

    private (List<(List<(GameObject, float)>,float)>,float) GetSpawnChanceDictionary()
    {
        List<(List<(GameObject,float)>, float)> SpawnChances = new();
        float total = 0;
        foreach(var monsterType in Monsters)
        {
            float typeTotal = 0;
            List < (GameObject, float) > variantChances= new();
            foreach (var variant in monsterType.Variants)
            {
                float chance = variant.SpawnCurve.Evaluate(Time.time / 60 / TimeToNextSpawn);
                if (chance > 0)
                {
                    variantChances.Add((variant.Monster, chance));
                    typeTotal += chance;
                    total += chance;
                }
            }
            SpawnChances.Add((variantChances, typeTotal));
            
        }
        return (SpawnChances, total);
    }
}




