using UnityEngine;
using System.Collections;

public class CashGeneration : MonoBehaviour
{
    public ResourceManager resourceManager;
    public int cashPerInterval; // Adjust this based on your game's balance.
    public float cashGenerationInterval; // Adjust the interval as needed.
    public float bonusCashMultiplier = 1.0f; // Default cash multiplier

    void Start()
    {
        StartCoroutine(GenerateCash());
 
    }

    private IEnumerator GenerateCash()
    {
        while (true)
        {
            float cashMultiplier = 1.0f - (resourceManager.player.happiness / 100.0f) + bonusCashMultiplier;
            int cashToGenerate = Mathf.RoundToInt(cashPerInterval * cashMultiplier);
            yield return new WaitForSeconds(cashGenerationInterval);
            resourceManager.AddCash(cashPerInterval);
        }
    }

}