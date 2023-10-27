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

    protected virtual void Update()
    {
        // Implement common player logic here
        UpdatePlayer();
        if (ownedTiles.Count == 0)
        {
            Debug.Log(playerTypeName + " has lost!");
            Destroy(gameObject);
        }
    }

    public abstract void StartPlayer();
    public abstract void UpdatePlayer(); // Implement player-specific input logic here

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
                            if (hexTile.cost <= currentPlayer.military)
                            {
                                currentPlayer.military -= hexTile.cost;
                            }
                            else
                            {
                                Debug.Log("Not enough military to buy this tile");
                                return false;
                            }
                        }

                        hexTile.owner.ownedTiles.Remove(hexTile);
                    }
                    else
                    {
                        if (this is Player currentPlayer)
                        {
                            if (hexTile.cost <= currentPlayer.money)
                            {
                                currentPlayer.money -= hexTile.cost;
                            }
                            else
                            {
                                Debug.Log("Not enough money to buy this tile");
                                return false;
                            }
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

        playerTile.InitTile(hexTile.q, hexTile.r); // Pass the original cost
        playerTile.transform.position = new Vector3(
            hexTile.q * 1.51f,
            0,
            Mathf.Sqrt(3) * (hexTile.r + hexTile.q / 2.0f)
        );

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
            if (index >= 0)
            {
                neighbor.neighbors[index] = playerTile;
            }
        }

        playerTile.color = color;
        playerTile.transform.SetParent(transform);

        // Debug.Log(playerTypeName + " bought " + playerTile.name);

        playerTile.SetOwner(this, hexTile);

        Destroy(hexTile.gameObject);
    }
}
