using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPlanetRotate : MonoBehaviour
{
    public GameObject SmallPlanet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SmallPlanet.transform.Rotate(0, .03f, 0, Space.Self);
    }
}
