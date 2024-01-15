using UnityEngine;
using System.Collections;

public class PopulationGeneration : MonoBehaviour
{

    public ResourceManager resourceManager;
    public int populationPerInterval; // Adjust this based on your game's balance.
    public float populationGenerationInterval; // Adjust the interval as needed.
    public float bonusPopulationMultiplier = 1.0f; // Default population multiplier

    private void Start()
    {
        StartCoroutine(GeneratePopulation());
    }
    
    private IEnumerator GeneratePopulation()
    {
        while (true)
        {
            float populationMultiplier = 1.0f - (resourceManager.player.happiness / 100.0f) + bonusPopulationMultiplier;
            int populationToGenerate = Mathf.RoundToInt(populationPerInterval * populationMultiplier);
            yield return new WaitForSeconds(populationGenerationInterval);
            resourceManager.AddPopulation(populationPerInterval);
        }
    }
}