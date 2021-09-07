using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace
{
    public class DontDestroy : MonoBehaviour
    {

    void Start()
    {
        for(int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
            {
                if(Object.FindObjectsOfType<DontDestroy>()[i] != this)
                {
                    if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                    {
                        Destroy(gameObject);
                    }
                }
                
            }
            
            DontDestroyOnLoad(gameObject);
    }
    }
}

