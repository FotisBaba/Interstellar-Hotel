using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRotate : MonoBehaviour
{

    public GameObject System;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        System.transform.Rotate(0, .025f, 0, Space.Self);
    }
}
