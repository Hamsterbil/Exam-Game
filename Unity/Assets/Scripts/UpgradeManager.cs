using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public ResourceManager resourceManager;
    public int cashUpgrades = 1;
    public int populationUpgrades = 1;
    public int militaryUpgrades = 1;
    public int actualUpgradePrice;


    public void ApplyCashUpgrade(string cashMultiplierAndPrice)
    {
        string[] cashMultiplierAndPriceArray = cashMultiplierAndPrice.Split(',');
        int cashMultiplierUpgrade = int.Parse(cashMultiplierAndPriceArray[0]);
        int cashUpgradePrice = int.Parse(cashMultiplierAndPriceArray[1]);

        // Calculate the actual price based on the number of upgrades purchased
        actualUpgradePrice = cashUpgradePrice * (cashUpgrades);

        if (resourceManager.GetCash() >= actualUpgradePrice)
        {
            cashUpgrades++;
            resourceManager.SubtractCash(actualUpgradePrice);
            resourceManager.cashGenerationMultiplier = cashMultiplierUpgrade * (cashUpgrades);
            
            actualUpgradePrice = cashUpgradePrice * (cashUpgrades);            
        }
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