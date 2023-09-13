using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public Player player;
    public int startingCash = 100;
    public int startingPopulation = 10;

    private int cash;
    private int population;

    public event Action<int> OnCashChanged; // Event to notify when cash changes
    public event Action<int> OnPopulationChanged; // Event to notify when population changes

    private void Start()
    {
        cash = startingCash;
        population = startingPopulation;
    }

    public int GetCash()
    {
        return cash;
    }

    public void AddCash(int amount)
    {
        cash += amount;
        OnCashChanged?.Invoke(cash);
        
    }

    public void SubtractCash(int amount)
    {
        if (cash >= amount)
        {
            cash -= amount;
            OnCashChanged?.Invoke(cash);
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
    }

    // You can add more methods here as needed, such as getters for resource values.
}