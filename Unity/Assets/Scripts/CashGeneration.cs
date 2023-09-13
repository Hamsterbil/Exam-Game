using UnityEngine;
using System.Collections;

public class CashGeneration : ResourceManager 
{
    private ResourceManager resourceManager;
    public int cashPerInterval = 100; // Adjust this based on your game's balance.
    public float cashGenerationInterval = 2.0f; // Adjust the interval as needed.

    private void Start()
    {
        resourceManager = GetComponent<ResourceManager>();
        StartCoroutine(GenerateCash());
    }

    private IEnumerator GenerateCash()
    {
        while (true)
        {
            yield return new WaitForSeconds(cashGenerationInterval);
            resourceManager.AddCash(cashPerInterval);
        }
    }
}

// Path: Unity/Assets/Scripts/ResourceManager.cs
