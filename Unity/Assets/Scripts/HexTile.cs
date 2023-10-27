using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    public int q;
    public int r;
    public int cost;
    public string typeName;
    public bool traversable;
    public Color color;
    public Color originalColor;
    private Vector3 originalScale;
    public List<HexTile> neighbors;
    public GridPlayer owner;

    public void InitTile(int Q, int R)
    {
        q = Q;
        r = R;
        name = typeName + " (" + q + "," + r + ")";
    }

    void Start()
    {
        // if (q == 0 && r == 0 && owner == null)
        // {
        //     name = "Center ----------------------------";
        //     color = Color.white;
        // }
        originalColor = color;
        originalScale = transform.localScale;
        GetNeighbors(GameObject.Find("HexGrid").GetComponent<HexGrid>().tiles);
        GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    protected virtual void Update()
    {
        GetComponentInChildren<MeshRenderer>().material.color = color;
    }

    public void SetOwner(GridPlayer player, HexTile previousTile)
    {
        owner = player;
        name = player.playerTypeName + previousTile.typeName + " (" + q + "," + r + ")";
        player.ownedTiles.Add(this);
        transform.position += new Vector3(0, 0.5f, 0);
    }

    void OnMouseEnter()
    {
        if (traversable)
        {
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

    public bool EligibleForPurchase(GridPlayer player, HexTile neighbor)
    {
        return this != null && traversable && this == neighbor && owner != player;
    }

    private List<HexTile> GetNeighbors(List<HexTile> allTiles)
    {
        neighbors = new List<HexTile>();

        int[] neighborDirections = { 0, 1, 2, 3, 4, 5 };

        foreach (int direction in neighborDirections)
        {
            int neighborQ = q;
            int neighborR = r;

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

            foreach (HexTile tile in allTiles)
            {
                if (tile.q == neighborQ && tile.r == neighborR)
                {
                    neighbors.Add(tile);
                    break;
                }
            }
        }

        return neighbors;
    }
}
