using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public Player player;
    public int startingMoney;
    public int startingPopulation;
    public int startingmilitary;
    public int startingHappiness;
    public int happiness;
    public int maxHappiness; // Maximum happiness
    public int maxMilitary; // Maximum military units based on population
    private int population;
    public int military;

    public event Action<int> OnCashChanged; // Event to notify when cash changes
    public event Action<int> OnPopulationChanged; // Event to notify when population changes
    public event Action<int> OnMilitaryUnitsChanged; // Event to notify when military units change
    public event Action<int> OnHappinessChanged; // Event to notify when happiness changes
    void Start()
    {
        player.money = startingMoney;
        player.population = startingPopulation;
        player.happiness = maxHappiness;
        player.military = startingmilitary;
        CalculateMilitaryUnits();
        happiness = maxHappiness; // Start with max happiness

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
        military = Mathf.Min(GetPopulation() / 10, maxMilitary);
    }

    public int GetMilitaryUnits()
    {
        return military;
    }

    public void AddMilitary(int amount)
    {
        military += amount;
    }

    public void SubtractMilitary(int amount)
    {
        if (military >= amount)
        {
            military -= amount;
        }
        else
        {
            // Handle military-related errors, if necessary
            Debug.LogWarning("Insufficient military units!");
        }
    }

    public void ModifyHappiness(int amount)
    {
        happiness += amount;
        happiness = Mathf.Clamp(happiness, 0, maxHappiness);
    }
}