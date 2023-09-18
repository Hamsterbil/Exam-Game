using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [HideInInspector]
    public Settings settings;

    public HexTile[] tilePrefabs;
    public List<HexTile> tiles;

    void Awake()
    {
        tiles = new List<HexTile>();

        int N = settings.mapSizeFromCenter;
        CreateGrid(N);

        AddEnemies(4);
    }

    void Update()
    {
        foreach (HexTile tile in tiles)
        {
            if (tile.typeName == "Water")
            {
                float yOffset = Mathf.Clamp(
                    Mathf.Sin(Time.time * settings.waveSpeed + tile.q * settings.waveAmount)
                        * settings.waveHeight,
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

    private void CreateGrid(int N)
    {
        for (int q = -N; q <= N; q++)
        {
            int r1 = Mathf.Max(-N, -q - N);
            int r2 = Mathf.Min(N, -q + N);

            for (int r = r1; r <= r2; r++)
            {
                int tileType = Random.Range(0, tilePrefabs.Length);
                HexTile tile = Instantiate(tilePrefabs[tileType]);

                tile.InitTile(q, r);
                tile.transform.position = new Vector3(q * 1.51f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));

                tile.transform.SetParent(transform);
                tiles.Add(tile);
            }
        }
    }

    private void AddEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject EnemyPlayer = new GameObject("Enemy " + i);
            Enemy enemy = EnemyPlayer.AddComponent<Enemy>();
            enemy.color = new Color(Random.value, Random.value, Random.value); // Generates random RGB color
        }
    }
}
