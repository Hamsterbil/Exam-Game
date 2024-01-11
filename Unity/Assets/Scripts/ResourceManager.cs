using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public Player player;
    public int startingMoney;
    public int startingPopulation;
    public int startingMilitary;
    public int startingHappiness;
    public int maxHappiness; // Maximum happiness
    public int maxMilitary; // Maximum military units based on population
    
 

    public float cashMultiplier = 1.0f; // Default cash multiplier
    public float populationMultiplier = 1.0f; // Default population multiplier
    public float tileCostMultiplier = 1.0f; // Default tile cost multiplier

    public event Action<int> OnCashChanged; // Event to notify when cash changes
    public event Action<int> OnPopulationChanged; // Event to notify when population changes
    public event Action<int> OnMilitaryUnitsChanged; // Event to notify when military units change
    public event Action<int> OnHappinessChanged; // Event to notify when happiness changes
    void Start()
    {
        player.money = startingMoney;
        player.population = startingPopulation;
        player.happiness = maxHappiness;
        player.military = startingMilitary;
        //CalculateMilitaryUnits();
       
    }
public void AdjustGameParameters(float happiness)
    {
        if (happiness > 50) // Example threshold, adjust as needed
        {
            cashMultiplier = 0.8f; // Adjust cash generation for high happiness
            populationMultiplier = 1.2f; // Increase population growth for high happiness
            tileCostMultiplier = 0.9f; // Reduce tile costs for high happiness
        }
        else
        {
            cashMultiplier = 1.2f; // Increase cash generation for low happiness
            populationMultiplier = 0.8f; // Decrease population growth for low happiness
            tileCostMultiplier = 1.1f; // Increase tile costs for low happiness
        }
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

  
    public void AddMilitary(int amount)
    {
        player.military += amount;
    }

    public void SubtractMilitary(int amount)
    {
        if (player.military >= amount)
        {
            player.military -= amount;
        }
        else
        {
            // Handle military-related errors, if necessary
            Debug.LogWarning("Insufficient military units!");
        }
    }

    public void ModifyHappiness(int amount)
    {
        player.happiness += amount;
        player.happiness = Mathf.Clamp(player.happiness, 0, maxHappiness);
    }

}