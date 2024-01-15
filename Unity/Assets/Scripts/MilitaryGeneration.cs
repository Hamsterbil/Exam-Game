using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MilitaryGeneration : MonoBehaviour
{
   public ResourceManager resourceManager;
    public float MilitaryPerInterval; // Adjust this based on your game's balance.
    public float MilitaryGenerationInterval; // Adjust the interval as needed.
    public float bonusMilitaryMultiplier = 1.0f; // Default military multiplier
    void Start()
    {
        StartCoroutine(GenerateMilitary());
 
    }
    private IEnumerator GenerateMilitary()
    {
        while (true)
        {
            float MilitaryMultiplier = 2.0f - (resourceManager.player.happiness / 100.0f) + bonusMilitaryMultiplier;
            int MilitaryToGenerate = Mathf.RoundToInt(MilitaryPerInterval * MilitaryMultiplier);
            yield return new WaitForSeconds(MilitaryGenerationInterval);
            resourceManager.AddMilitary(MilitaryToGenerate);
            
            
        }
    }
}