using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : HexCell
{
    private string name = "City"; // Serialized field for the city name
    private int tileCost = 10; // Serialized field for the city cost
    private bool isTraversable = true; // Serialized field for traversable

    public City()
    {
        typeName = name; // Set typeName from the serialized field
        cost = tileCost; // Set cost from the serialized field
        traversable = isTraversable; // Set traversable from the serialized field
    }
}
