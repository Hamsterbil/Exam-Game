using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : HexCell
{
    public Snow() {
        typeName = "Snow";
        traversable = true;
        color = Color.white;
        cost = 3;
    }
}
