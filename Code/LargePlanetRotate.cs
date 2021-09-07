using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePlanetRotate : MonoBehaviour
{

    public GameObject LargePlanet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LargePlanet.transform.Rotate(0, .008f, 0, Space.Self);
    }
}
