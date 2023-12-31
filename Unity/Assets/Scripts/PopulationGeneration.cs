using UnityEngine;
using System.Collections;

public class PopulationGeneration : MonoBehaviour
{

    public ResourceManager resourceManager;
    public int populationPerInterval; // Adjust this based on your game's balance.
    public float populationGenerationInterval; // Adjust the interval as needed.

    private void Start()
    {
        StartCoroutine(GeneratePopulation());
    }
    
    private IEnumerator GeneratePopulation()
    {
        while (true)
        {
            float populationMultiplier = 1.0f - (resourceManager.player.happiness / 100.0f);
            int populationToGenerate = Mathf.RoundToInt(populationPerInterval * populationMultiplier);
            yield return new WaitForSeconds(populationGenerationInterval);
            resourceManager.AddPopulation(populationPerInterval);
        }
    }
}