using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money; // Player's initial money
    public int population; // Player's initial population
    public int happiness; // Player's initial happiness
    public int military; // Player's initial military
    public Color color; // Color for player-owned tiles
    public LayerMask hexCellLayerMask; // Layer mask for hex cells

    public List<HexCell> ownedTiles = new List<HexCell>(); // List of tiles owned by the player

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hexCellLayerMask))
            {
                HexCell hexCell = hit.collider.gameObject.GetComponent<HexCell>();
                bool addCell = false;
                foreach (HexCell ownedTile in ownedTiles)
                {
                    foreach (HexCell neighbor in ownedTile.neighbors)
                    {
                        if (hexCell != null && hexCell.traversable && hexCell == neighbor && hexCell.owner != this && hexCell.cost <= money)
                        {
                            money -= hexCell.cost;
                            addCell = true;
                        }
                    }
                }
                if (addCell) {
                    hexCell.SetOwner(this);
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    }
}
