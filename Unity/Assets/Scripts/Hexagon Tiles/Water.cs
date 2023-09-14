using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : HexCell
{
    private string name = "Water"; // Serialized field for the water name
    private int tileCost = 1; // Serialized field for the water cost
    private Color tileColor = Color.blue; // Serialized field for the water color
    private bool isTraversable = false; // Serialized field for traversable

    public Water()
    {
        typeName = name; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
