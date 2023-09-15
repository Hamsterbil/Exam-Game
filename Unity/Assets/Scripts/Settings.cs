using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    public int mapSizeFromCenter = 10;
    [Range(0, 1)]
    public float waveAmount;
    [Range(0.1f, 2)]
    public float waveHeight;
    [Range(0, 5)]
    public float waveSpeed;
}
