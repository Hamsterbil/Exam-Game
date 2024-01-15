using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour 
{
    public float cashMultiplierUpgrade = 5f; // Increase in cash multiplier per upgrade
    public float populationMultiplierUpgrade = 5f; // Increase in population multiplier per upgrade
    public float militaryMultiplierUpgrade = 5f; // Increase in military multiplier per upgrade

    private int cashUpgrades = 100;
    private int populationUpgrades = 100;
    private int militaryUpgrades = 100;

    public void ApplyCashUpgrade()
    {
        cashUpgrades++;
    }

    public void ApplyPopulationUpgrade()
    {
        populationUpgrades++;
    }

    public void ApplyMilitaryUpgrade()
    {
        militaryUpgrades++;
    }

    // Getters for the current multipliers after applying upgrades
    public float GetCashMultiplier()
    {
        return 1.0f + (cashUpgrades * cashMultiplierUpgrade);
    }

    public float GetPopulationMultiplier()
    {
        return 1.0f + (populationUpgrades * populationMultiplierUpgrade);
    }

    public float GetMilitaryMultiplier()
    {
        return 1.0f + (militaryUpgrades * militaryMultiplierUpgrade);
    }
}