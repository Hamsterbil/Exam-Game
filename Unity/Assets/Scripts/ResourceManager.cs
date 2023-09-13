using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public Player player;
    public int startingCash;
    public int startingPopulation;

    public int maxMilitaryUnits = 1000; // Maximum military units based on population

    private int cash;
    private int population;
    private int militaryUnits = 0;


    public event Action<int> OnCashChanged; // Event to notify when cash changes
    public event Action<int> OnPopulationChanged; // Event to notify when population changes
    public event Action<int> OnMilitaryUnitsChanged; // Event to notify when military units change

    void Start()
    {
        player.money = startingCash;
        player.population = startingPopulation;
        CalculateMilitaryUnits();
    }

    void Update()
    {
     
    }

    public int GetCash()
    {
        return player.money;
    }

    public void AddCash(int amount)
    {
        player.money += amount;
    }

    public void SubtractCash(int amount)
    {
        if (player.money >= amount)
        {
            player.money -= amount;
        }
        else
        {
            // Handle insufficient funds
            Debug.LogWarning("Insufficient cash!");
        }
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
        if (player.population >= amount)
        {
            player.population -= amount;
        }
        else
        {
            // Handle population-related errors, if necessary
            Debug.LogWarning("Insufficient population!");
        }
    }

    public void CalculateMilitaryUnits()
    {
        // Calculate military units based on population
        militaryUnits = Mathf.Min(GetPopulation() / 10, maxMilitaryUnits);
    }

    public int GetMilitaryUnits()
    {
        return militaryUnits;
    }

    // You can add more methods here as needed, such as getters for resource values.
}