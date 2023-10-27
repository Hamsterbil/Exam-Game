using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    int amountOfEnemies;
    int extraLayers;
    int traversableAmount;
    int N;

    [HideInInspector]
    public Settings settings;

    public HexTile[] tilePrefabs;
    public List<HexTile> tiles;

    void Awake()
    {
        amountOfEnemies = settings.amountOfEnemies;
        extraLayers = settings.mapExtraLayers;    
        N = settings.mapSizeFromCenter;   

        tiles = new List<HexTile>();

        CreateMap();
        AddEnemies(amountOfEnemies);
    }

    void Update()
    {
        foreach (HexTile tile in tiles)
        {
            if (tile.typeName == "Water")
            {
                float yOffset = Mathf.Clamp(
                    Mathf.Sin(Time.time * settings.waveSpeed + tile.q * settings.waveAmount) * settings.waveHeight,
                    -0.5f,
                    0f
                );
                tile.transform.position = new Vector3( tile.transform.position.x, yOffset, tile.transform.position.z);
            }
        }
    }

    private void CreateMap()
    {
        for (int q = -N - extraLayers; q <= N + extraLayers; q++)
        {
            int r1 = Mathf.Max(-N - extraLayers, -q - N - extraLayers);
            int r2 = Mathf.Min(N + extraLayers, -q + N + extraLayers);

            for (int r = r1; r <= r2; r++)
            {
                CreateTile(q, r);
            }
        }
    }

    private void CreateTile(int q, int r)
    {
        int tileType = GetPerlinID(q, r);
        if (q < -N || q > N || r < -N || r > N || q + r < -N || q + r > N)
        {
            tileType = 0; // Water tile
        }
        HexTile tile = Instantiate(tilePrefabs[tileType]);
        
        tile.InitTile(q, r);
        tile.transform.position = new Vector3(q * 1.5f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));

        if (tile.traversable)
        {
            traversableAmount++;
        }

        tile.transform.SetParent(transform);
        tiles.Add(tile);
    }
    
    private void AddEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            //If more enemies than traversable tiles, stop
            if (i >= traversableAmount)
            {
                Debug.LogError("Too many enemies for this map size!");
                Debug.LogError("The maximum amount of enemies for this map size is " + traversableAmount);
                //Stop the game
                Application.Quit();
                return;
            }
            GameObject EnemyPlayer = new GameObject("Enemy " + i + " ");
            Enemy enemy = EnemyPlayer.AddComponent<Enemy>();
            enemy.color = new Color(Random.value, Random.value, Random.value); // Generates random RGB color
            enemy.enemyTurnTime = Random.Range(1, 5);
        }
    }

    private int GetPerlinID(int q, int r)
    {
        float perlin = GeneratePerlin(q, r);

        if (perlin <= settings.perlinWaterLevel)
        {
            return 0;
        }
        else if (perlin <= settings.perlinWaterLevel + 0.05f)
        {
            return 1;
        }
        else if (perlin <= settings.perlinLandLevel + 0.1f)
        {
            return 2;
        }
        else if (perlin <= settings.perlinLandLevel + 0.2f)
        {
            return 3;
        }
        else if (perlin <= settings.perlinLandLevel + 0.3f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    private float GeneratePerlin(float x, float z) {
        float x_offset = settings.perlinOffsetX;
        float z_offset = settings.perlinOffsetZ;

        float noise = 
        1 * Mathf.PerlinNoise(1 * (((x + x_offset)) / settings.perlinScale), 1 * ((z + z_offset) / settings.perlinScale)) +
        0.5f * Mathf.PerlinNoise(2 * (x + x_offset) / settings.perlinScale1, 2 * (z + z_offset) / settings.perlinScale1) +
        0.25f * Mathf.PerlinNoise(4 * (x + x_offset) / settings.perlinScale2, 4 * (z + z_offset) / settings.perlinScale2);
        noise = noise / (1 + 0.5f + 0.25f);
        
        return Mathf.Pow(noise, settings.perlinPow);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2 * settings.mapSizeFromCenter * 1.5f, 1, 2 * settings.mapSizeFromCenter * Mathf.Sqrt(3)));

        //Draw perlin noise
        for (int q = -settings.mapSizeFromCenter; q <= settings.mapSizeFromCenter; q++)
        {
            int r1 = Mathf.Max(-settings.mapSizeFromCenter, -q - settings.mapSizeFromCenter);
            int r2 = Mathf.Min(settings.mapSizeFromCenter, -q + settings.mapSizeFromCenter);

            for (int r = r1; r <= r2; r++)
            {
                float perlin = GeneratePerlin(q, r);

                Gizmos.color = Color.Lerp(Color.white, Color.black, perlin);
                //if perlin is lower than settings.perlinWaterLevel, make color blue. Do the same for grass, forest, sand, snow and mountain
                if (perlin <= settings.perlinWaterLevel && perlin >= settings.perlinWaterLevel - 0.03f)
                {
                    Gizmos.color = Color.Lerp(Color.blue, Color.white, perlin);
                }
                else if (perlin <= settings.perlinWaterLevel + 0.03f && perlin >= settings.perlinWaterLevel - 0.02f)
                {
                    Gizmos.color = Color.Lerp(Color.blue, Color.white, perlin);
                }
                else if (perlin <= settings.perlinLandLevel + 0.15 && perlin >= settings.perlinLandLevel - 0.15f)
                {
                    Gizmos.color = Color.Lerp(Color.green, Color.white, perlin);
                }
                else if (perlin <= settings.perlinLandLevel + 0.3f)
                {
                    //Dark green
                    Gizmos.color = Color.Lerp(Color.green, Color.black, perlin);
                }
                else if (perlin <= settings.perlinLandLevel + 0.4f)
                {
                    Gizmos.color = Color.Lerp(Color.gray, Color.white, perlin);
                }
                else
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.white, perlin);
                }
                //Draw as cube with height
                Gizmos.DrawCube(new Vector3(q * 1.5f, 3, Mathf.Sqrt(3) * (r + q / 2.0f)), new Vector3(1.51f, perlin * 30, 1.51f));
            }
        }
    }
}