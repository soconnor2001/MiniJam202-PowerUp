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
        if (AverageTimeBetwenSpawns.Evaluate(Time.timeSinceLevelLoad) < SpawnRateVariability / 2)
        {
            SpawnRateVariability = 2* AverageTimeBetwenSpawns.Evaluate(Time.timeSinceLevelLoad / (MinutesToMaxDifficulty * 60));
        }
    }
    
    void ResetTimeToNextSpawn()
    {
        TimeToNextSpawn = ((UnityEngine.Random.value * SpawnRateVariability) / 2f);
        TimeToNextSpawn = (TimeToNextSpawn - (TimeToNextSpawn / 2)) + (AverageTimeBetwenSpawns.Evaluate(Time.timeSinceLevelLoad/(MinutesToMaxDifficulty*60)));
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
        MonsterType typeToSpawn = GetMonsterTypeToSpawn();
        List<MonsterVariant> variants = typeToSpawn.Variants;
        float typeTotal = typeToSpawn.GetCurrentSpawnChance(Time.timeSinceLevelLoad);
        string monsterName = typeToSpawn.Name;
        float monsterSpawnHeight = typeToSpawn.SpawnHeight;

        int numToSpawn = UnityEngine.Random.Range(0, (int)MaxMonsterToSpawn.Evaluate(Time.timeSinceLevelLoad / (MinutesToMaxDifficulty * 60)) +1);
        List<GameObject> monstersToSpawn = new();
        for (int i = 0; i < numToSpawn; i++)
        {
            monstersToSpawn.Add(GetMonsterVariantToSpawn(variants, typeTotal).Monster);
        }

        MonsterType monsterType = Monsters.Find(x=>x.Name == monsterName);
        //Pick an unweighted spawnpoint and Instantiate allmonster there, but not on top of each other (hopefully)
        
        Transform chosenSpawnPoint = typeToSpawn.GetRandomSpawnPoint(Player,minDistanceFromPlayer);
        
        

        
        foreach(var monster in monstersToSpawn)
        { 
            GameObject newMonster = Instantiate(monster, RandomPointInCircle.GetRandomPointInCircle(chosenSpawnPoint.position, spawnPointRadius), Quaternion.LookRotation(Vector3.forward), MonstersParent);
            newMonster.transform.position = new(newMonster.transform.position.x, monsterType.SpawnHeight, newMonster.transform.position.z);
            if (newMonster.TryGetComponent<RotatingToTargetBehavior>(out var rotationBehavior)) rotationBehavior.target = playerCharacter.transform;
            if (newMonster.TryGetComponent<EnemyCollider>(out var enemyCollider)) enemyCollider.collidedWithPlayer.AddListener(playerCharacter.GetComponent<Health>().ReceiveDamage);
        }
    }
    
    private MonsterVariant GetMonsterVariantToSpawn(List<MonsterVariant> Variants,float total)
    {
        float randVal = UnityEngine.Random.value * total;
        int index = 0;
        MonsterVariant TypeToSpawn = default!;

        while (index < Variants.Count && randVal > 0)
        {
            randVal = randVal - Variants[index].GetCurrentSpawnChance(Time.timeSinceLevelLoad);
            if (randVal <= 0)
            {
                TypeToSpawn = Variants[index];
            }
        }
        if (randVal > 0)
        {
            throw new ArithmeticException("GetMonsterTypeToSpawn() did not find a monster to Spawn");
        }

        return TypeToSpawn;
    }
    private MonsterType GetMonsterTypeToSpawn()
    {
        
        float total = GetChanceTotal();

        float randVal = UnityEngine.Random.value * total;
        int index = 0;
        MonsterType TypeToSpawn = default!;
        
        while (index < Monsters.Count && randVal > 0)
        {
            randVal = randVal - Monsters[index].GetCurrentSpawnChance(Time.timeSinceLevelLoad);
            if (randVal <= 0)
            {
                TypeToSpawn = Monsters[index];
            }
        }
        if (randVal > 0)
        {
            throw new ArithmeticException("GetMonsterTypeToSpawn() did not find a monster to Spawn");
        }

        return TypeToSpawn;
    }

    private float GetChanceTotal()
    {
        float total = 0;
        foreach (var monster in Monsters)
        {
            total += monster.GetCurrentSpawnChance(Time.timeSinceLevelLoad);
        }

        return total;
    }

    private (List<(List<(GameObject, float)>,float,string, float)>,float) GetSpawnChanceDictionary()
    {
        List<(List<(GameObject,float)>, float, string, float)> SpawnChances = new();
        float total = 0;
        foreach(var monsterType in Monsters)
        {
            float typeTotal = 0;
            List < (GameObject, float) > variantChances= new();
            foreach (var variant in monsterType.Variants)
            {
                float chance = variant.SpawnCurve.Evaluate(Time.timeSinceLevelLoad / 60 / TimeToNextSpawn);
                if (chance > 0)
                {
                    variantChances.Add((variant.Monster, chance));
                    typeTotal += chance;
                    total += chance;
                }
            }
            SpawnChances.Add((variantChances, typeTotal,monsterType.Name, monsterType.SpawnHeight));
            
        }
        return (SpawnChances, total);
    }
}




