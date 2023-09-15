using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : HexTile
{
    private string tileName = "Mountain"; // Serialized field for the mountain tileName
    private int tileCost = 1; // Serialized field for the mountain cost
    private Color tileColor = Color.gray; // Serialized field for the mountain color
    private bool isTraversable = false; // Serialized field for traversable

    public Mountain()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
