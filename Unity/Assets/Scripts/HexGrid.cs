using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [HideInInspector]
    public Settings settings;

    public HexCell[] tilePrefabs;
    public List<HexCell> cells = new List<HexCell>();

    void Awake()
    {
        GameObject EnemyPlayer = new GameObject("Enemy");
        Enemy enemy = EnemyPlayer.AddComponent<Enemy>();
        int N = settings.mapSizeFromCenter;

        for (int q = -N; q <= N; q++)
        {
            int r1 = Mathf.Max(-N, -q - N);
            int r2 = Mathf.Min(N, -q + N);

            for (int r = r1; r <= r2; r++)
            {
                int tileType = Random.Range(0, tilePrefabs.Length);
                HexCell cell = Instantiate(tilePrefabs[tileType]);

                cell.InitTile(q, r);
                cell.transform.position = new Vector3(q * 1.51f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));

                cell.transform.SetParent(transform);
                cells.Add(cell);
            }
        }
    }

    void Update()
    {
        foreach (HexCell cell in cells)
        {
            if (cell.typeName == "Water")
            {
                float yOffset = Mathf.Clamp(Mathf.Sin(Time.time * settings.waveSpeed + cell.q * settings.waveAmount) * settings.waveHeight, -0.5f, 0f);
                cell.transform.position = new Vector3(cell.transform.position.x, yOffset, cell.transform.position.z);
            }
        }
    }
}
