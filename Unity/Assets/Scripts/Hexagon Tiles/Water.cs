using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : HexTile
{
    private string tileName = "Water"; // Serialized field for the water tileName
    private int tileCost = 1; // Serialized field for the water cost
    private Color tileColor = Color.blue; // Serialized field for the water color
    private bool isTraversable = false; // Serialized field for traversable

    public Water()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
