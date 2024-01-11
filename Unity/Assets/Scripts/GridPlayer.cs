using System.Collections.Generic;
using UnityEngine;

public abstract class GridPlayer : MonoBehaviour
{
    public abstract string playerTypeName { get; }
    public Color color;

    public HexTile ownedTilePrefab;

    [HideInInspector]
    public HexGrid grid;
    public List<HexTile> ownedTiles = new List<HexTile>();

    [HideInInspector]
    public Settings settings;

    private int playerTileAmount;

    protected virtual void Start()
    {
        grid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        if (grid.tiles.Count > 0)
        {
            int randomTile = Random.Range(0, grid.tiles.Count);
            while (
                grid.tiles[randomTile].owner != null || grid.tiles[randomTile].traversable == false
            )
            {
                randomTile = Random.Range(0, grid.tiles.Count);
            }
            AddTile(grid.tiles[randomTile]);
        }

        StartPlayer();
    }

    protected virtual void Update() // Common player logic here
    {
        UpdatePlayer();
        if (ownedTiles.Count == 0)
        {
            Debug.Log(playerTypeName + " has lost!");
            Destroy(gameObject);
        }
    }

    public abstract void StartPlayer();
    public abstract void UpdatePlayer(); // Player-specific input logic here

    public void CheckAndAddTile(HexTile hexTile)
    {
        if (CanAddTile(hexTile))
        {
            AddTile(hexTile);
        }
        else
        {
            Debug.LogWarning("Can't buy " + hexTile.name);
        }
    }

    protected bool CanAddTile(HexTile hexTile)
    {
        //kdkd
        if (hexTile.EligibleForPurchase(this))
        {
            if (hexTile.owner != null)
            {
                if (this is Player player)
                {
                    if (player.military > hexTile.cost)
                    {
                        player.resourceManager.SubtractMilitary(hexTile.cost);
                    }
                    else
                    {
                        return false;
                    }
                }
                hexTile.owner.ownedTiles.Remove(hexTile);
            }
            else if (this is Player player)
            {
                if (player.money > hexTile.cost)
                {
                    player.resourceManager.SubtractCash(hexTile.cost);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    protected void AddTile(HexTile hexTile)
    {
        Debug.Log(playerTypeName + " bought " + hexTile.name);

        HexTile playerTile = Instantiate(ownedTilePrefab);
        playerTile.InitTile(hexTile.q, hexTile.r, color, hexTile.originalScale, transform); // Pass the original values to the new tile

        // Replace hexTile from the grid's tiles list if it exists
        if (grid.tiles.Contains(hexTile))
        {
            int index = grid.tiles.IndexOf(hexTile);
            // Add playerTile to the grid's tiles list
            grid.tiles[index] = playerTile;
        }

        // Replace the Tile in the neighbor lists with the new owned Tile
        foreach (HexTile neighbor in hexTile.neighbors)
        {
            if (neighbor.neighbors.Contains(hexTile))
            {
                int index = neighbor.neighbors.IndexOf(hexTile);
                neighbor.neighbors[index] = playerTile;
            }
        }

        if (this is Player currentPlayer)
        {
            if (AllowTileCostUpdate())
            {
                grid.UpdateTileCosts();
            }
        }

        playerTile.SetOwner(this, hexTile);
        Destroy(hexTile.gameObject);
    }

    public bool AllowTileCostUpdate()
    {
        if (ownedTiles.Count % 20 == 0 && ownedTiles.Count > playerTileAmount)
        {
            playerTileAmount = ownedTiles.Count;
            return true;
        }
        else
        {
            return false;
        }
    }
}
