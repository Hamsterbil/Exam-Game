using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : HexCell
{
    private string name = "Sand"; // Serialized field for the sand name
    private int tileCost = 1; // Serialized field for the sand cost
    private Color tileColor = Color.yellow; // Serialized field for the sand color
    private bool isTraversable = true; // Serialized field for traversable

    public Sand()
    {
        typeName = name; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
