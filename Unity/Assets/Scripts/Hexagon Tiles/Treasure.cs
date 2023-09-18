using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : HexTile
{
    private string tileName = "Treasure"; // Serialized field for the treasure tileName
    private int tileCost = 1; // Serialized field for the treasure cost
    private Color tileColor = Color.yellow; // Serialized field for the treasure color
    private bool isTraversable = true; // Serialized field for traversable

    public Treasure()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
