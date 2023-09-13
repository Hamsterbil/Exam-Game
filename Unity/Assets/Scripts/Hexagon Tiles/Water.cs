using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : HexCell
{
    public Water()
    {
        typeName = "Water";
        traversable = false;
        color = Color.blue;
        cost = 0;
    }
}
