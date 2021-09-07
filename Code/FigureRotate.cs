using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureRotate : MonoBehaviour
{
    public GameObject figure;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        figure.transform.Rotate(0, 0.008f, 0, Space.Self);
    }
}
