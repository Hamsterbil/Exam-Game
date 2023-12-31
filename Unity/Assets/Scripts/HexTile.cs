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
    public float originalScale;
    public List<HexTile> neighbors;
    public GridPlayer owner;

    public void InitTile(int Q, int R, Color color, float scale, Transform parentObject)
    {
        q = Q;
        r = R;
        name = typeName + " (" + q + "," + r + ")";
        this.color = color;
        originalColor = color;
        changeScale(scale);
        originalScale = scale;

        transform.position = new Vector3(
            q * 1.5f,
            0,
            r * Mathf.Sqrt(3) + q * Mathf.Sqrt(3) / 2
        );
    }

    void Start()
    {
        // if (q == 0 && r == 0 && owner == null)
        // {
        //     name = "Center ----------------------------";
        //     color = Color.white;
        // }
        neighbors = GetNeighbors(GameObject.Find("HexGrid").GetComponent<HexGrid>().tiles, 1);
        // GetComponentInChildren<MeshRenderer>().material.color = color;
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
    }

    void OnMouseEnter()
    {
        if (traversable)
        {
            // originalScale = transform.localScale;
            color = Color.Lerp(color, Color.black, 0.2f);
            if (owner != null)
            {
                //Change scale of every tile with the same owner
                foreach (HexTile tile in owner.ownedTiles)
                {
                    tile.changeScale(tile.originalScale * 1.2f);
                }
            }
            else
            {
                changeScale(originalScale * 1.2f);
            }
        }
    }

    void OnMouseExit()
    {
        if (traversable)
        {
            color = originalColor;
            if (owner != null)
            {
                //Change scale of every tile with the same owner
                foreach (HexTile tile in owner.ownedTiles)
                {
                    tile.changeScale(tile.originalScale);
                }
            }
            else
            {
                changeScale(originalScale);
            }
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

    public void changeScale(float scale)
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, scale);
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, scale + 1, 0);
        }
    }

    public bool EligibleForPurchase(GridPlayer player, HexTile neighbor)
    {
        return this != null && traversable && this == neighbor && owner != player;
    }

    private List<HexTile> GetNeighbors(List<HexTile> allTiles, int distance)
    {        
        List<HexTile> tileNeighbors = new List<HexTile>();
        for (int q = -distance; q <= distance; q++)
        {
            int r1 = Mathf.Max(-distance, -q - distance);
            int r2 = Mathf.Min(distance, -q + distance);

            for (int r = r1; r <= r2; r++)
            {
                if (q != 0 || r != 0)
                {
                    foreach (HexTile tile in allTiles)
                    {
                        if (tile.q == q + this.q && tile.r == r + this.r)
                        {
                            tileNeighbors.Add(tile);
                        }
                    }
                }
            }
        }
        return tileNeighbors;
    }
}
