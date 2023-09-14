using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : HexCell
{
    public City() {
        typeName = "City";
        traversable = false;
        cost = 10;
    }

    
}
