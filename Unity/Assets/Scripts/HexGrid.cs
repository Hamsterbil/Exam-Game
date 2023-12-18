using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    int amountOfEnemies;
    int extraLayers;
    int traversableAmount;
    int N;
    float waterLevel;
    float landLevel;
    float perlinPow;
    float x_offset;
    float z_offset;

    [HideInInspector]
    public Settings settings;

    public HexTile[] tilePrefabs;
    public List<HexTile> tiles;

    void Awake()
    {
        amountOfEnemies = settings.amountOfEnemies;
        extraLayers = settings.mapExtraLayers;
        N = settings.mapSizeFromCenter;
        perlinPow = settings.perlinPow;
        waterLevel = settings.perlinWaterLevel;
        landLevel = settings.perlinLandLevel;

        if (settings.randomizeOffset)
        {
            x_offset = Random.Range(0, 1000);
            z_offset = Random.Range(0, 1000);
        }
        else
        {
            x_offset = settings.perlinOffsetX;
            z_offset = settings.perlinOffsetZ;
        }

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
                    (
                        Mathf.Sin(Time.time * settings.waveSpeed + tile.q * settings.waveAmount)
                        + Mathf.Cos(0.5f * Time.time * settings.waveSpeed + tile.q * settings.waveAmount)
                        + Mathf.Sin(0.25f * Time.time * settings.waveSpeed + tile.q * settings.waveAmount * 3f + tile.r * settings.waveAmount)
                    ) * settings.waveHeight,
                    -0.5f,
                    0f
                );
                tile.transform.position = new Vector3(
                    tile.transform.position.x,
                    yOffset,
                    tile.transform.position.z
                );
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
        tile.transform.position = new Vector3(q * 1.51f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));
        if (tileType != 0)
        {
            tile.transform.position = new Vector3(
                tile.transform.position.x,
                tile.transform.position.y + (tileType / 2f),
                tile.transform.position.z
            );
        }
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
                Debug.LogError(
                    "The maximum amount of enemies for this map size is " + traversableAmount
                );
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
        float perlinGen = GeneratePerlin(q, r, false);
        float perlinMoisture = GeneratePerlin(q + 0.5f, r + 0.5f, false);
        float perlinRivers = GeneratePerlin(q, r, false);
        
        float perlin = perlinGen + perlinMoisture;

        if (perlin <= settings.perlinWaterLevel && perlin >= 0.03f)
        {
            return 0;
        }
        else if (
            perlin <= settings.perlinWaterLevel + 0.03f
            && perlin >= settings.perlinLandLevel - 0.01f
        )
        {
            return 1;
        }
        else if (
            perlin <= settings.perlinLandLevel + 0.15 && perlin >= settings.perlinLandLevel - 0.15f
        )
        {
            return 2;
        }
        else if (perlin <= settings.perlinLandLevel + 0.2f)
        {
            return 3;
        }
        else if (perlin <= settings.perlinLandLevel + 0.4f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    private float GeneratePerlin(float x, float z, bool draw)
    {
        x += x_offset;
        z += z_offset;
        if (draw)
        {
            x = settings.perlinOffsetX;
            z = settings.perlinOffsetZ;
        }
        float noise =
            1
                * Mathf.PerlinNoise(
                    1 * (x / settings.perlinScale),
                    1 * (z / settings.perlinScale)
                )
            + 0.5f
                * Mathf.PerlinNoise(
                    2 * (x / settings.perlinScale1),
                    2 * (z / settings.perlinScale1)
                )
            + 0.25f
                * Mathf.PerlinNoise(
                    4 * (x / settings.perlinScale2),
                    4 * (z / settings.perlinScale2)
                );
        noise = noise / (1 + 0.5f + 0.25f);

        return Mathf.Pow(noise, settings.perlinPow);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            Vector3.zero,
            new Vector3(
                2 * (settings.mapSizeFromCenter + settings.mapExtraLayers) * 1.5f,
                1,
                2 * (settings.mapSizeFromCenter + settings.mapExtraLayers) * Mathf.Sqrt(3)
            )
        );

        //Draw perlin noise
        for (
            int q = -settings.mapSizeFromCenter - settings.mapExtraLayers;
            q <= settings.mapSizeFromCenter + settings.mapExtraLayers;
            q++
        )
        {
            int r1 = Mathf.Max(
                -settings.mapSizeFromCenter - settings.mapExtraLayers,
                -q - settings.mapSizeFromCenter - settings.mapExtraLayers
            );
            int r2 = Mathf.Min(
                settings.mapSizeFromCenter + settings.mapExtraLayers,
                -q + settings.mapSizeFromCenter + settings.mapExtraLayers
            );

            for (int r = r1; r <= r2; r++)
            {
                float perlinElevation = GeneratePerlin(q, r, true);
                // float perlinMoisture = GeneratePerlin(q / 4f, r * 0.8f, true);
                // float perlinRivers = GeneratePerlin(q, r, true);
                // perlinRivers = perlinRivers * 2;

                if (perlinElevation <= settings.perlinWaterLevel && perlinElevation >= 0.03f)
                {
                    Gizmos.color = Color.Lerp(Color.blue, Color.white, perlinElevation);
                }
                else if (
                    perlinElevation <= settings.perlinWaterLevel + 0.03f
                    && perlinElevation >= settings.perlinLandLevel - 0.01f
                )
                {
                    Gizmos.color = Color.Lerp(Color.yellow, Color.white, perlinElevation);
                }
                else if (
                    perlinElevation <= settings.perlinLandLevel + 0.15
                    && perlinElevation >= settings.perlinLandLevel - 0.15f
                )
                {
                    Gizmos.color = Color.Lerp(Color.green, Color.white, perlinElevation);
                }
                else if (perlinElevation <= settings.perlinLandLevel + 0.3f)
                {
                    //Dark green
                    Gizmos.color = Color.Lerp(Color.green, Color.black, perlinElevation);
                }
                else if (perlinElevation <= settings.perlinLandLevel + 0.4f)
                {
                    Gizmos.color = Color.Lerp(Color.gray, Color.black, perlinElevation);
                }
                else
                {
                    Gizmos.color = Color.Lerp(Color.white, Color.white, perlinElevation);
                }
                if (
                    q < -settings.mapSizeFromCenter
                    || q > settings.mapSizeFromCenter
                    || r < -settings.mapSizeFromCenter
                    || r > settings.mapSizeFromCenter
                    || q + r < -settings.mapSizeFromCenter
                    || q + r > settings.mapSizeFromCenter
                )
                {
                    Gizmos.color = Color.Lerp(Color.blue, Color.white, perlinElevation);
                }
                //Draw as cube with height
                Gizmos.DrawCube(
                    new Vector3(q * 1.5f, 3, Mathf.Sqrt(3) * (r + q / 2.0f)),
                    new Vector3(1.51f, perlinElevation * 20, 1.51f)
                );
                // //Draw as cube with moisture
                // Gizmos.color = Color.Lerp(Color.blue, Color.white, perlinMoisture);
                // Gizmos.DrawCube(new Vector3(q * 1.5f, 50, Mathf.Sqrt(3) * (r + q / 2.0f)),
                //     new Vector3(1.51f, perlinMoisture * 20, 1.51f));

                // //Draw as cube with rivers
                // Gizmos.color = Color.Lerp(Color.blue, Color.white, perlinRivers);
                // Gizmos.DrawCube(new Vector3(q * 1.5f, 100, Mathf.Sqrt(3) * (r + q / 2.0f)),
                //     new Vector3(1.51f, perlinRivers * 20, 1.51f));
            }
        }
    }
}
