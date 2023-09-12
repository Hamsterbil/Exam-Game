using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : HexCell
{
    public Sand() {
        typeName = "Sand";
        traversable = true;
        color = Color.yellow;
        cost = 1;
    }
}
