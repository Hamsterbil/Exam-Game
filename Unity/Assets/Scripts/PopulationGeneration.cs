using UnityEngine;
using System.Collections;

public class PopulationGeneration : MonoBehaviour
{

    private ResourceManager resourceManager;

    public int populationPerInterval = 100; // Adjust this based on your game's balance.

    public float populationGenerationInterval = 2.0f; // Adjust the interval as needed.

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        StartCoroutine(GeneratePopulation());
    }
    
    private IEnumerator GeneratePopulation()
    {
        while (true)
        {
            yield return new WaitForSeconds(populationGenerationInterval);
            resourceManager.AddPopulation(populationPerInterval);
        }
    }
}

// Path: Unity/Assets/Scripts/ResourceManager.cs