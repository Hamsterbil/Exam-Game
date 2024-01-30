using System.Collections.Generic;
using UnityEngine;

public class Player : GridPlayer
{
    public override string playerTypeName => gameObject.name;
    public int cash;
    public int population;
    public int military;
    public int happiness;

    public CameraController playerCamera;
    public ResourceManager resourceManager;
    public LayerMask hexTileLayerMask;

    public override void StartPlayer()
    {
        playerCamera.FindPlayer(this);
        HighlightNeighbors();
    }

    public override void UpdatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hexTileLayerMask))
            {
                HexTile hexTile = hit.collider.gameObject.GetComponentInParent<HexTile>();
                if (hexTile != null)
                {
                    CheckAndAddTile(hexTile);
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    }

    public void HighlightNeighbors()
    {
        foreach (HexTile ownedTile in ownedTiles)
        {
            foreach (HexTile neighbor in ownedTile.neighbors)
            {
                if (neighbor != null && neighbor.traversable && neighbor.owner != this)
                {
                    neighbor.color = Color.red;
                }
            }
        }
    }

    public void RemoveHighlights(HexTile ownedTile)
    {
        foreach (HexTile neighbor in ownedTile.neighbors)
        {
            neighbor.color = neighbor.originalColor;
        }
    }
}
