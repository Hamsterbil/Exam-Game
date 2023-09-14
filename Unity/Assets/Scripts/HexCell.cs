using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public int q;
    public int r;

    // Define common properties
    public string typeName;
    public Color color;
    public int cost;
    public bool traversable;
    public List<HexCell> neighbors;
    public GridPlayer owner;
    public Player player;

    private Color originalColor;
    private Vector3 originalScale;

    // Method to initialize the cell as a specific tile type

    public void InitTile(HexCell tileType, int Q, int R)
    {
        q = Q;
        r = R;
        color = tileType.color;
        // cost = tileType.cost;
        traversable = tileType.traversable;
        typeName = tileType.typeName;
        name = typeName + " (" + q + "," + r + ")";
    }

    void Start()
    {
        // ... code to initialize the cell ...
        if (q == 0 && r == 0 && owner == null)
        {
            name = "Center ----------------------------";
            color = Color.white;
        }
        GetNeighbors(GameObject.Find("HexGrid").GetComponent<Grid>().cells);
    }

    protected virtual void Update()
    {
        GetComponentInChildren<MeshRenderer>().material.color = color;
        if (owner != null && typeName == "Player City")
        {
            foreach (HexCell neighbor in neighbors)
            {
                if (neighbor != null && neighbor.traversable && neighbor.owner == null)
                {
                    neighbor.color = Color.red;
                }
            }
        }
    }

    void OnMouseEnter()
    {
        Debug.Log(typeName);
        if (traversable)
        {
            originalColor = color;
            originalScale = transform.localScale;

            color = Color.Lerp(color, Color.black, 0.2f);
            transform.localScale = new Vector3(1, originalScale.y * 1.1f, 1);
        }
    }

    void OnMouseExit()
    {
        if (traversable)
        {
            color = originalColor;
            transform.localScale = originalScale;
        }
    }

    void OnMouseDown()
    {
        if (traversable)
        {
            // ... code to handle mouse down ...
            Debug.Log("Clicked on " + name);
        }
    }

    public void SetOwner(GridPlayer player)
    {
        owner = player; // Set the owner of the tile
        owner.money -= cost; // Subtract the cost of the tile from the player's money if not enemy
        typeName = player.playerTypeName + typeName;
        name = typeName + " (" + q + "," + r + ")";
        player.ownedTiles.Add(this); // Add the tile to the player's list of owned tiles

        transform.position += new Vector3(0, 0.5f, 0);
    }

    public List<HexCell> GetNeighbors(List<HexCell> allCells)
    {
        neighbors = new List<HexCell>();

        // Define the six directions for neighbors
        int[] neighborDirections = { 0, 1, 2, 3, 4, 5 };

        foreach (int direction in neighborDirections)
        {
            int neighborQ = q;
            int neighborR = r;

            // Adjust neighborQ and neighborR based on the direction
            switch (direction)
            {
                case 0:
                    neighborQ += 1;
                    break;
                case 1:
                    neighborQ += 1;
                    neighborR -= 1;
                    break;
                case 2:
                    neighborR -= 1;
                    break;
                case 3:
                    neighborQ -= 1;
                    break;
                case 4:
                    neighborQ -= 1;
                    neighborR += 1;
                    break;
                case 5:
                    neighborR += 1;
                    break;
            }

            // Search for the neighbor in the list of all cells
            foreach (HexCell cell in allCells)
            {
                if (cell.q == neighborQ && cell.r == neighborR)
                {
                    neighbors.Add(cell);
                    break; // Found the neighbor, no need to continue searching
                }
            }
        }

        return neighbors;
    }
}
