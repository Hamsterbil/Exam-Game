using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : HexTile
{
    private string tileName = "Snow"; // Serialized field for the snow name
    private int tileCost = 1; // Serialized field for the snow cost
    private Color tileColor = Color.white; // Serialized field for the snow color
    private bool isTraversable = true; // Serialized field for traversable

    public Snow()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
