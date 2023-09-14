using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : HexCell
{
    private string name = "Mountain"; // Serialized field for the mountain name
    private int tileCost = 1; // Serialized field for the mountain cost
    private Color tileColor = Color.gray; // Serialized field for the mountain color
    private bool isTraversable = false; // Serialized field for traversable

    public Mountain()
    {
        typeName = name; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
