using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : HexCell
{
    public Grass() {
        typeName = "Grass";
        traversable = true;
        color = Color.green;
        cost = 1;
    }
}
