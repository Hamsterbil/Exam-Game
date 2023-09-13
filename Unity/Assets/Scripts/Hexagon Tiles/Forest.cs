using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : HexCell
{
    public Forest() {
        typeName = "Forest";
        traversable = true;
        color = new Color(0.0f, 0.5f, 0.0f);
        cost = 2;
    }
}
