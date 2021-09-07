using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace {
    public class Node : MonoBehaviour
    {
        public GameObject previousNode;

        public GameObject node;

        public GameObject nextNode;

        public GameObject innerEntranceObject;

        public GameObject outerEntranceObject;

        [SerializeField] private bool hasEntrance = false;


        public GameObject[] positions;

        private int playersOnTile = 0;

        [SerializeField] public GameObject innerAreaObject;
        [SerializeField] public GameObject outerAreaObject;

        [SerializeField] private NodeType type;
        [SerializeField] private bool innerEntrance;
        [SerializeField] private bool outerEnrance;
        [SerializeField] private int ID;

        public int PlayersOnTile { get => playersOnTile; set => playersOnTile = value; }
        public int ID1 { get => ID; set => ID = value; }
        public NodeType Type { get => type; set => type = value; }
        public bool HasEntrance { get => hasEntrance; set => hasEntrance = value; }

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log(type);
            // Debug.Log(innerEntrance);
            //Debug.Log(ID);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public enum NodeType
        {
            BuyNode,
            BuildNode,
            FreeBuildNode,
            FreeEntranceNode
        }

        void OnMouseDown()
        {
            Debug.Log("MouseDOOOOOOOOOWN");
            if (GameObject.Find("System").GetComponent<GameFlow>().waitingForTile)
            {
                Debug.Log("wait for tile true");
                GameObject.Find("System").GetComponent<GameFlow>().EntranceTileToBuild = this.node;
                GameObject.Find("System").GetComponent<GameFlow>().waitingForTile = false;
                GameObject.Find("System").GetComponent<GameFlow>().tileSelected = true;
            }
        }
    }
}