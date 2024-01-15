using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour 
{
    public MilitaryGeneration militaryGeneration;
    public ResourceManager resourceManager;
    public int cashUpgrades = 1;
    public int populationUpgrades = 1;
    public int militaryUpgrades = 1;

    public void ApplyCashUpgrade(int cashMultiplierUpgrade)
    {
        cashUpgrades++;
        resourceManager.cashGenerationMultiplier = (cashMultiplierUpgrade * cashUpgrades);
        
    }

    public void ApplyPopulationUpgrade(int populationMultiplierUpgrade)
    {
        populationUpgrades++;
        resourceManager.populationGenerationMultiplier = (populationMultiplierUpgrade * populationUpgrades);

    }

    public void ApplyMilitaryUpgrade(int militaryMultiplierUpgrade)
    {
        militaryUpgrades++;
        resourceManager.militaryGenerationMultiplier = (militaryMultiplierUpgrade * militaryUpgrades);
        
    }
}