using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public HexCell cellPrefab;
    public List<HexCell> cells = new List<HexCell>();
    public int N;

    [Range(0, 1)]
    public float waves;

    [Range(1, 3)]
    public float waveSpeed = 1;

    [Range(0, 5)]
    public float waveHeight;

    // Start is called before the first frame update
    void Start()
    {
        for (int q = -N; q <= N; q++)
        {
            int r1 = Mathf.Max(-N, -q - N);
            int r2 = Mathf.Min(N, -q + N);
            for (int r = r1; r <= r2; r++)
            {
                HexCell cell = Instantiate(cellPrefab);
                cell.q = q;
                cell.r = r;
                cell.name = "HexCell " + q + " " + r;
                cell.transform.position = new Vector3(q * 1.51f, 0, Mathf.Sqrt(3) * (r + q / 2.0f));
                cells.Add(cell);

                if (Random.Range(0, 100) < 50)
                {
                    cell.isWater = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (HexCell cell in cells)
        {
            if (cell.isWater)
            {
                cell.transform.localScale = new Vector3(
                    1,
                    Mathf.Sin(Time.time * waveSpeed + cell.q * waves) * waveHeight + 10,
                    1
                );
                cell.GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            }
        }
    }
}
