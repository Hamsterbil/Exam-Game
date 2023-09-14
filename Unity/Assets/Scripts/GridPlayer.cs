using System.Collections.Generic;
using UnityEngine;

public abstract class GridPlayer : MonoBehaviour
{
    public int money;
    public int population;
    public int happiness;
    public int military;

    [HideInInspector]
    public HexGrid grid;
    [HideInInspector]
    public Settings settings;

    public HexCell ownedTilePrefab;
    public List<HexCell> ownedTiles = new List<HexCell>();
    public abstract string playerTypeName { get; }

    protected virtual void Start()
    {
        grid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        if (grid.cells.Count > 0)
        {
            int randomCell = Random.Range(0, grid.cells.Count);
            while (grid.cells[randomCell].owner != null && !grid.cells[randomCell].traversable)
            {
                randomCell = Random.Range(0, grid.cells.Count);
            }
            AddCell(grid.cells[randomCell]);
        }

        StartPlayer();
    }


    protected virtual void Update()
    {
        // Implement common player logic here
        UpdatePlayer();
    }

    public abstract void StartPlayer();
    public abstract void UpdatePlayer(); // Implement player-specific input logic here

    public void CheckAndAddCell(HexCell hexCell)
    {
        if (CanAddCell(hexCell))
        {
            AddCell(hexCell);
        }
    }

    protected bool CanAddCell(HexCell hexCell)
    {
        foreach (HexCell ownedTile in ownedTiles)
        {
            foreach (HexCell neighbor in ownedTile.neighbors)
            {
                if (
                    hexCell != null
                    && hexCell.traversable
                    && hexCell == neighbor
                    && hexCell.owner != this
                )
                {
                    if (hexCell.owner != null && hexCell.cost <= military)
                    {
                        hexCell.owner.ownedTiles.Remove(hexCell);
                        military -= hexCell.cost;
                        return true;
                    }
                    else if (hexCell.cost <= money)
                    {
                        money -= hexCell.cost;
                        return true;
                    }
                    //FIX HERE: else never happens except if you have 0 money and 0 military
                    else
                    {
                        Debug.Log("Not enough money or military to buy this tile");
                        return false;
                    }
                }
            }
        }
        return false;
    }

    protected void AddCell(HexCell hexCell)
    {
        HexCell playerCell = Instantiate(ownedTilePrefab);

        playerCell.InitTile(hexCell.q, hexCell.r); // Pass the original cost
        playerCell.transform.position = new Vector3(
            hexCell.q * 1.51f,
            0,
            Mathf.Sqrt(3) * (hexCell.r + hexCell.q / 2.0f)
        );

        // Replace the cell in the grid's cells list, player-owned cell list, and cell neighbor list with the new owned cell
        grid.cells[grid.cells.IndexOf(hexCell)] = playerCell;

        foreach (HexCell neighbor in hexCell.neighbors)
        {
            neighbor.neighbors[neighbor.neighbors.IndexOf(hexCell)] = playerCell;
        }

        playerCell.transform.SetParent(transform);
        playerCell.SetOwner(this, hexCell);

        Destroy(hexCell.gameObject);
    }
}
