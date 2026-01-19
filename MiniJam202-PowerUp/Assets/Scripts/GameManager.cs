using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public List<MonsterType> Monsters;
    public Transform MonstersParent;
    public float MinutesToMaxDifficulty;
    public float spawnPointRadius;

    public float minDistanceFromPlayer;
    public Transform Player;

    public AnimationCurve AverageTimeBetwenSpawns;
    public float SpawnRateVariability;
    public AnimationCurve MaxMonsterToSpawn;

    float TimeToNextSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimeToNextSpawn();
    }
    private void OnValidate()
    {
        if (AverageTimeBetwenSpawns.Evaluate(Time.time) < SpawnRateVariability / 2)
        {
            SpawnRateVariability = 2* AverageTimeBetwenSpawns.Evaluate(Time.time);
        }
    }
    
    void ResetTimeToNextSpawn()
    {
        TimeToNextSpawn = ((UnityEngine.Random.value * SpawnRateVariability) / 2f);
        TimeToNextSpawn = (TimeToNextSpawn - (TimeToNextSpawn / 2)) + AverageTimeBetwenSpawns.Evaluate(Time.time);
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
        (List<(GameObject, float)>, float,string) TypesList = GetMonsterTypeToSpawn();
        List<(GameObject, float)> weightedTypes = TypesList.Item1;
        float typeTotal = TypesList.Item2;
        string monsterName = TypesList.Item3;

        int numToSpawn = UnityEngine.Random.Range(0, (int)MaxMonsterToSpawn.Evaluate(Time.time)+1);
        List<GameObject> monstersToSpawn = new();
        for (int i = 0; i < numToSpawn; i++)
        {
            monstersToSpawn.Add(GetMonsterVariantToSpawn(weightedTypes, typeTotal));
        }

        MonsterType monsterType = Monsters.Find(x=>x.Name == monsterName);
        //Pick an unweighted spawnpoint and Instantiate allmonster there, but not on top of each other (hopefully)
        List<Transform> spawnpointsToChooseFrom = monsterType.SpawnPoints;
        Transform chosenSpawnPoint;
        do
        {
            chosenSpawnPoint = spawnpointsToChooseFrom[UnityEngine.Random.Range(0, spawnpointsToChooseFrom.Count)];
        } while (minDistanceFromPlayer > Vector3.Distance(chosenSpawnPoint.position, Player.position));
        

        
        foreach(var monster in monstersToSpawn)
        { 
            GameObject newMonster = Instantiate(monster, RandomPointInCircle.GetRandomPointInCircle(chosenSpawnPoint.position, spawnPointRadius), Quaternion.LookRotation(Vector3.forward), MonstersParent);
            newMonster.transform.position = new(newMonster.transform.position.x, monsterType.SpawnHeight, newMonster.transform.position.z);
            if (newMonster.TryGetComponent<RotatingToTargetBehavior>(out var rotationBehavior)) rotationBehavior.target = playerCharacter.transform;
            if (newMonster.TryGetComponent<EnemyCollider>(out var enemyCollider)) enemyCollider.collidedWithPlayer.AddListener(playerCharacter.GetComponent<Health>().ReceiveDamage);
        }
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
    private (List<(GameObject, float)>,float,string) GetMonsterTypeToSpawn()
    {
        var chances = GetSpawnChanceDictionary();
        List<(List<(GameObject, float)>, float,string)> rates = chances.Item1;
        float total = chances.Item2;

        float randVal = UnityEngine.Random.value * total;
        int index = 0;
        (List<(GameObject, float)>,float,string) TypeToSpawn = default!;
        
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

    private (List<(List<(GameObject, float)>,float,string)>,float) GetSpawnChanceDictionary()
    {
        List<(List<(GameObject,float)>, float, string)> SpawnChances = new();
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
            SpawnChances.Add((variantChances, typeTotal,monsterType.Name));
            
        }
        return (SpawnChances, total);
    }
}




