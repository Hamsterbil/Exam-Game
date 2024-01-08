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

    protected virtual void Start()
    {
        grid = GameObject.Find("HexGrid").GetComponent<HexGrid>();
        if (grid.tiles.Count > 0)
        {
            int randomTile = Random.Range(0, grid.tiles.Count);
            while (grid.tiles[randomTile].owner != null || grid.tiles[randomTile].traversable == false)
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
    }

    protected bool CanAddTile(HexTile hexTile)
    {
        foreach (HexTile ownedTile in ownedTiles)
        {
            foreach (HexTile neighbor in ownedTile.neighbors)
            {
                if (hexTile.EligibleForPurchase(this, neighbor))
                {
                    // Debug.Log(hexTile.name + " is traversable and a neighbor of " + ownedTile.name);
                    if (hexTile.owner != null)
                    {
                        if (hexTile.owner is Player)
                        {
                            Player player = (Player)hexTile.owner;
                            player.RemoveHighlights(hexTile);
                        }
                        if (this is Player currentPlayer)
                        {
                            currentPlayer.resourceManager.SubtractMilitary(hexTile.cost);
                        }

                        hexTile.owner.ownedTiles.Remove(hexTile);
                    }
                    else
                    {
                        if (this is Player currentPlayer)
                        {
                            currentPlayer.resourceManager.SubtractCash(hexTile.cost);
                        }
                    }

                    return true;
                }
            }
        }
        return false;
    }

    protected void AddTile(HexTile hexTile)
    {
        HexTile playerTile = Instantiate(ownedTilePrefab);
        playerTile.InitTile(hexTile.q, hexTile.r, color, hexTile.originalScale, transform); // Pass the original values to the new tile

        // Remove hexTile from the grid's tiles list if it exists
        if (grid.tiles.Contains(hexTile))
        {
            grid.tiles.Remove(hexTile);
        }

        // Add playerTile to the grid's tiles list
        grid.tiles.Add(playerTile);

        // Replace the Tile in the neighbor lists with the new owned Tile
        foreach (HexTile neighbor in hexTile.neighbors)
        {
            int index = neighbor.neighbors.IndexOf(hexTile);
            neighbor.neighbors[index] = playerTile;
        }

        // Debug.Log(playerTypeName + " bought " + playerTile.name);

        playerTile.SetOwner(this, hexTile);

        Destroy(hexTile.gameObject);
    }
}
