using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour 
{
    public MilitaryGeneration militaryGeneration;
    public PopulationGeneration populationGeneration;
    public CashGeneration cashGeneration;
    private int cashUpgrades = 1;
    private int populationUpgrades = 1;
    private int militaryUpgrades = 1;

    public void ApplyCashUpgrade(int cashMultiplierUpgrade)
    {
        cashUpgrades++;
        cashGeneration.bonusCashMultiplier = (cashMultiplierUpgrade * cashUpgrades);
        
    }

    public void ApplyPopulationUpgrade(int populationMultiplierUpgrade)
    {
        populationUpgrades++;
        populationGeneration.bonusPopulationMultiplier = (populationMultiplierUpgrade * populationUpgrades);

    }

    public void ApplyMilitaryUpgrade(int militaryMultiplierUpgrade)
    {
        militaryUpgrades++;
        militaryGeneration.bonusMilitaryMultiplier = (militaryMultiplierUpgrade * militaryUpgrades);
        
    }
}