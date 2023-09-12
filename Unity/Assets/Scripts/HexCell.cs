using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{
    public int q;
    public int r;
    public Color color;
    public bool isWater;
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(ChangeColor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(2);
        color = new Color(
            Random.Range(0.0f, 1.0f), 
            Random.Range(0.0f, 1.0f), 
            Random.Range(0.0f, 1.0f)
        );
        GetComponentInChildren<MeshRenderer>().material.color = color;
        StartCoroutine(ChangeColor());
    }
}