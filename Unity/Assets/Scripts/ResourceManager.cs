using UnityEngine;
using System;
using System.Collections;
public class ResourceManager : MonoBehaviour
{
    public Player player;
    
    public int startingCash;
    public int startingPopulation;
    public int startingMilitary;
    public int startingHappiness;
    public int maxHappiness; // Maximum happiness
    public int maxMilitary; // Maximum military units based on population

    public float cashGenerationMultiplier; // Multiplier for cash generation
    public float populationGenerationMultiplier; // Multiplier for population generation
    public float militaryGenerationMultiplier; // Multiplier for military generation

    public int generatePerInterval; // Amount of resources to generate per interval
    public int generationInterval; // Interval to generate resources
    void Start()
    {
        player.cash = startingCash;
        player.population = startingPopulation;
        player.happiness = maxHappiness;
        player.military = startingMilitary;
        StartCoroutine(GenerateCash());
        StartCoroutine(GeneratePopulation());
        StartCoroutine(GenerateMilitary());
    }

    public IEnumerator GenerateCash()
    {
        while (true)
        {
            float cashMultiplier = 1.0f - (player.happiness / 100.0f) + cashGenerationMultiplier;
            int cashToGenerate = Mathf.RoundToInt(generatePerInterval * cashMultiplier);
            yield return new WaitForSeconds(generationInterval);
            AddCash(cashToGenerate);
        }
    }

    public IEnumerator GeneratePopulation()
    {
        while (true)
        {
            float populationMultiplier = 1.0f - (player.happiness / 100.0f) + populationGenerationMultiplier;
            int populationToGenerate = Mathf.RoundToInt(generatePerInterval * populationMultiplier);
            yield return new WaitForSeconds(generationInterval);
            AddPopulation(populationToGenerate);
        }
    }

    public IEnumerator GenerateMilitary()
    {
        while (true)
        {
            float militaryMultiplier = 2.0f - (player.happiness / 100.0f) + militaryGenerationMultiplier;
            int militaryToGenerate = Mathf.RoundToInt(generatePerInterval * militaryMultiplier);
            yield return new WaitForSeconds(generationInterval);
            AddMilitary(militaryToGenerate);
        }
    }
    
    void Update()
    {
     
    }

    public int GetCash()
    {
        return player.cash;
    }

    public void AddCash(int amount)
    {
        player.cash += amount;

    }

    public void SubtractCash(int amount)
    {
        player.cash -= amount;
    }

    public int GetPopulation()
    {
        return player.population;
    }

    public void AddPopulation(int amount)
    {
        player.population += amount;

    }

    public void SubtractPopulation(int amount)
    {
        player.population -= amount;
    }
  
    public void AddMilitary(int amount)
    {
        player.military += amount;
    }

    public void SubtractMilitary(int amount)
    {
        player.military -= amount;
    }

    public void ModifyHappiness(int amount)
    {
        player.happiness += amount;
        player.happiness = Mathf.Clamp(player.happiness, 0, maxHappiness);
    }
}