using System.Collections.Generic;
using UnityEngine;

public class Player : GridPlayer
{
    public CameraController playerCamera;
    public override string playerTypeName => "Player ";
    public LayerMask hexCellLayerMask;

    public override void StartPlayer()
    {
        playerCamera.FindPlayer(this);
    }

    public override void UpdatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hexCellLayerMask))
            {
                HexCell hexCell = hit.collider.gameObject.GetComponent<HexCell>();
                CheckAndAddCell(hexCell);
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    }
}
