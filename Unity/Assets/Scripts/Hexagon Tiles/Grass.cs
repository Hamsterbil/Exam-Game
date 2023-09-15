using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : HexTile
{
    private string tileName = "Grass"; // Serialized field for the grass tileName
    private int tileCost = 1; // Serialized field for the grass cost
    private Color tileColor = Color.green; // Serialized field for the grass color
    private bool isTraversable = true; // Serialized field for traversable

    public Grass()
    {
        typeName = tileName; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
