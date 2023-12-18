using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    [Range(0, 1)]
    public float waveAmount;
    [Range(0.1f, 2)]
    public float waveHeight;
    [Range(0, 5)]
    public float waveSpeed;
    public int mapSizeFromCenter;
    public int mapExtraLayers;
    public int amountOfEnemies;
    public float enemyTurnTime;

    [Header("Perlin Noise Settings")]
    public float perlinScale;
    public float perlinScale1;
    public float perlinScale2;
    [Range(1, 10)]
    public float perlinPow;
    [Range(0, 1)]
    public float perlinWaterLevel;
    [Range(0, 1)]
    public float perlinLandLevel;

    public float perlinOffsetX;
    public float perlinOffsetZ;
    public bool randomizeOffset;
}