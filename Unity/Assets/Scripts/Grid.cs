using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public HexCell[] tilePrefabs; // Array of tile prefabs for different types
    public Settings settings;
    public List<HexCell> cells = new List<HexCell>();
    int N;

    // Start is called before the first frame update
    void Awake()
    {
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

        foreach (HexCell cell in cells)
        {
            // Get neighbors and set them for the current cell
            cell.GetNeighbors(cells);
        }
        int randomCell = Random.Range(0, cells.Count);
        cells[randomCell].SetOwner(GameObject.Find("Player").GetComponent<Player>());
    }

    // Update is called once per frame
    void Update()
    {
        // ... code to update the grid ...
        //Update water tiles to move with sine wave
        foreach (HexCell cell in cells)
        {
            if (cell.typeName == "Water")
            {
                // cell.transform.localScale = new Vector3(
                //     1,
                //     Mathf.Sin(Time.time * settings.waveSpeed + cell.q * settings.waveAmount) * settings.waveHeight + 1,
                //     1
                // );
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
