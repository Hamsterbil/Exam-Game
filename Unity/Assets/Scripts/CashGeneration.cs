using UnityEngine;
using System.Collections;

public class CashGeneration : MonoBehaviour
{
    public ResourceManager resourceManager;
    public int cashPerInterval; // Adjust this based on your game's balance.
    public float cashGenerationInterval; // Adjust the interval as needed.

    void Start()
    {
        StartCoroutine(GenerateCash(cashGenerationInterval));
    }

    private IEnumerator GenerateCash(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            resourceManager.AddCash(cashPerInterval);
        }
    }
}