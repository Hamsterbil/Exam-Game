using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : HexCell
{
    public Treasure() {
        typeName = "Treasure";
        traversable = true;
        color = Color.yellow;
        cost = 1;
    }
}
