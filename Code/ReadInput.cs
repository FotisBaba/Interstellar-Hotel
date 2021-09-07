using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace {
    public class ReadInput : MonoBehaviour
    {

        public string input;

        public global::System.String Input { get => input; set => input = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public ReadInput() { } 

        public void readStringInput(string s)
        {
            this.Input = s;
        }
    }
}
