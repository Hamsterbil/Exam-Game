using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : HexTile
{
    private string tileName = "Forest"; // Serialized field for the forest tileName
    private int tileCost = 1; // Serialized field for the forest cost
    private Color tileColor = new Color(0.0f, 0.5f, 0.0f); // Serialized field for the forest color
    private bool isTraversable = true; // Serialized field for traversable

    public Forest()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
