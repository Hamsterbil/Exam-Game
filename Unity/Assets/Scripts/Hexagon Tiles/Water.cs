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

    // protected override void Update() {
    //     base.Update();
        
    //     //Move with sine wave
    //     transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * 0.01f, transform.position.z);
    // }
}
