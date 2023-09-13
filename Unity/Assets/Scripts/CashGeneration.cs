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
            yield return new WaitForSeconds(cashGenerationInterval);
            resourceManager.AddCash(cashPerInterval);
        }
    }
}