using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountain : HexCell
{
    public Mountain() {
        typeName = "Mountain";
        traversable = false;
        color = Color.gray;
        cost = 0;
    }
}
