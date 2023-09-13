using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money = 100; // Player's initial money
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
                if (hexCell != null)
                {
                    // Check if the hexCell is unowned or owned by another player
                    if (hexCell.owner == null || hexCell.owner != this)
                    {
                        if (hexCell.traversable) {
                            // Set this player as the owner of the hexCell
                            hexCell.SetOwner(this);
                        }
                    }
                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        
    }
}
