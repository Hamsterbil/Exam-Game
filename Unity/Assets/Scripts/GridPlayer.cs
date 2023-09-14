using System.Collections.Generic;
using UnityEngine;

public abstract class GridPlayer : MonoBehaviour
{
    public int money;
    public int population;
    public int happiness;
    public int military;
    public Grid grid;
    public HexCell ownedTilePrefab;
    public Color color;
    public List<HexCell> ownedTiles = new List<HexCell>();
    public abstract string playerTypeName { get; }

    protected virtual void Start()
    {
        grid = GameObject.Find("HexGrid").GetComponent<Grid>();
        if (grid.cells.Count > 0)
        {
            int randomCell = Random.Range(0, grid.cells.Count);
            while (grid.cells[randomCell].owner != null && grid.cells[randomCell].traversable == false)
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

    protected void CheckAndAddCell(HexCell hexCell)
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
                if (hexCell != null && hexCell.traversable && hexCell == neighbor && hexCell.owner != this && hexCell.cost <= money)
                {
                    return true;
                }
            }
        }
        return false;
    }

    protected void AddCell(HexCell hexCell)
    {
            Debug.Log(playerTypeName + "----------------- is taking a tile");
            HexCell playerCell = Instantiate(ownedTilePrefab);
            playerCell.InitTile(hexCell, hexCell.q, hexCell.r);
            playerCell.transform.position = new Vector3(hexCell.q * 1.51f, 0, Mathf.Sqrt(3) * (hexCell.r + hexCell.q / 2.0f));

            grid.cells[grid.cells.IndexOf(hexCell)] = playerCell;
            if (ownedTiles.Contains(hexCell))
            {
                ownedTiles[ownedTiles.IndexOf(hexCell)] = playerCell;
            }
            

            playerCell.SetOwner(this);
            
            Destroy(hexCell.gameObject);
    }
}
