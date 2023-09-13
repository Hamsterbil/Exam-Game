using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public Player player;
    public int startingCash;
    public int startingPopulation;

<<<<<<< HEAD
=======
    public int maxMilitaryUnits = 1000; // Maximum military units based on population

    private int cash;
>>>>>>> main
    private int population;
    private int militaryUnits = 0;


    public event Action<int> OnCashChanged; // Event to notify when cash changes
    public event Action<int> OnPopulationChanged; // Event to notify when population changes
    public event Action<int> OnMilitaryUnitsChanged; // Event to notify when military units change

    void Start()
    {
        player.money = startingCash;
        population = startingPopulation;
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
            OnCashChanged?.Invoke(player.money);
        }
        else
        {
            // Handle insufficient funds
            Debug.LogWarning("Insufficient cash!");
        }
    }

    public int GetPopulation()
    {
        return population;
    }

    public void AddPopulation(int amount)
    {
        population += amount;
        OnPopulationChanged?.Invoke(population);
    }

    public void SubtractPopulation(int amount)
    {
        if (population >= amount)
        {
            population -= amount;
            OnPopulationChanged?.Invoke(population);
        }
        else
        {
            // Handle population-related errors, if necessary
            Debug.LogWarning("Insufficient population!");
        }

        public void CalculateMilitaryUnits()
        {
            // Calculate military units based on population
            militaryUnits = Mathf.Min(GetPopulation() / 10, maxMilitaryUnits);
            OnMilitaryUnitsChanged?.Invoke(militaryUnits);
        }

        public int GetMilitaryUnits()
        {
            return militaryUnits;
        }
    }

    // You can add more methods here as needed, such as getters for resource values.
}