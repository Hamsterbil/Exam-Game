using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : HexCell
{
    private string name = "Grass"; // Serialized field for the grass name
    private int tileCost = 1; // Serialized field for the grass cost
    private Color tileColor = Color.green; // Serialized field for the grass color
    private bool isTraversable = true; // Serialized field for traversable

    public Grass()
    {
        typeName = name; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        color = tileColor; // Set color from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
