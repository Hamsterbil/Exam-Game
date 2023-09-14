using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public HexCell[] tilePrefabs; // Array of tile prefabs for different types
    public Settings settings;
    public List<HexCell> cells = new List<HexCell>();
    int N;

    void Awake()
    {
        GameObject EnemyPlayer = new GameObject("Enemy");
        Enemy enemy = EnemyPlayer.AddComponent<Enemy>();
        N = settings.mapSizeFromCenter;
        for (int q = -N; q <= N; q++)
        {
            int r1 = Mathf.Max(-N, -q - N);
            int r2 = Mathf.Min(N, -q + N);
            for (int r = r1; r <= r2; r++)
            {
                // Randomly select a tile type
                int tileType = Random.Range(0, tilePrefabs.Length);

                // Create an instance of the selected prefab
                HexCell cell = Instantiate(tilePrefabs[tileType]);

                // Set cell properties
                cell.InitTile(cell, q, r);
                cell.transform.position = new Vector3(q * 1.51f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));

                // Add the cell to the grid's cells list
                cells.Add(cell);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update water tiles to move with sine wave
        foreach (HexCell cell in cells)
        {
            if (cell.typeName == "Water")
            {
                cell.transform.position = new Vector3(
                    cell.transform.position.x,
                    Mathf.Clamp(
                        Mathf.Sin(Time.time * settings.waveSpeed + cell.q * settings.waveAmount)
                            * settings.waveHeight,
                        -0.5f,
                        0f
                    ),
                    cell.transform.position.z
                );
            }
        }
    }
}
