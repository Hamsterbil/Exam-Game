using UnityEngine;
using System.Collections;

public class CashGeneration : MonoBehaviour
{
    public ResourceManager resourceManager;
    public int cashPerInterval; // Adjust this based on your game's balance.
    public float cashGenerationInterval; // Adjust the interval as needed.

    void Start()
    {
        StartCoroutine(GenerateCash());
    }

    private IEnumerator GenerateCash()
    {
        while (true)
        {
            float cashMultiplier = 1.0f - (resourceManager.happiness / 100.0f);
            int cashToGenerate = Mathf.RoundToInt(cashPerInterval * cashMultiplier);
            yield return new WaitForSeconds(cashGenerationInterval);
            resourceManager.AddCash(cashPerInterval);
        }
    }
}