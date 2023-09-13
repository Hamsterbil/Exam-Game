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
    public Player owner;

    // ... other common properties and behaviors ...

    // Method to initialize the cell as a specific tile type
    public void InitTile(HexCell tileType)
    {
        color = tileType.color;
        cost = tileType.cost;
        traversable = tileType.traversable;
        typeName = tileType.typeName;
    }

    void Start()
    {
        // ... code to initialize the cell ...
        if (q == 0 && r == 0)
        {
            name = "Center ----------------------------";
            GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
    }

    protected virtual void Update()
    {
        // ... code to update the cell ...
        name = typeName + " (" + q + "," + r + ")";
        if (owner != null)
        {
            //lerp color
            GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(GetComponentInChildren<MeshRenderer>().material.color, owner.color, 0.1f);
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().material.color = color;
        }
    }

    private Color originalColor;
    private Vector3 originalPosition;
    void OnMouseEnter()
    {
        Debug.Log(typeName);
        if (traversable)
        {
            originalColor = GetComponentInChildren<MeshRenderer>().material.color;
            originalPosition = transform.position;
            color = Color.Lerp(color, Color.red, 0.1f);
            transform.position += new Vector3(0, 0.5f, 0);
        }
    }

    void OnMouseExit()
    {
        if (traversable) {
            color = originalColor;
            transform.position = originalPosition;
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

    public void SetCoordinates(int q, int r)
    {
        this.q = q;
        this.r = r;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetOwner(Player player)
    {
        // ... code to set the owner of the tile ...
        owner = player; // Set the owner of the tile
        player.ownedTiles.Add(this); // Add the tile to the player's list of owned tiles
        typeName = "Player " + typeName;
        transform.position += new Vector3(0, 0.5f, 0);
    }

    public void getNeighbors()
    {
        // Implement neighbor finding logic if needed
    }
}
