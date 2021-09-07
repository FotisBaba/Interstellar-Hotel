using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace
{
    public class Area : MonoBehaviour
    {
        private string ownerName = "None";
        //private int ownerID;
        public GameObject area;
        public GameObject userInterface;
        public int buyingPrice;
        public GameObject[] buildings;
        public GameObject[] neighborTiles;
        [SerializeField] private int[] buildingPrice;



        private int builds = 0;

        public string OwnerName { get => ownerName; set => ownerName = value; }
        public int Builds { get => builds; set => builds = value; }
        public int[] BuildingPrice { get => buildingPrice; set => buildingPrice = value; }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ownerLost()
        {
            this.ownerName = "None";
            this.Builds = 0;
            foreach(GameObject b in buildings)
            {
                b.SetActive(false);
            }
            foreach(GameObject nt in neighborTiles)
            {
                if (nt.GetComponent<Node>().HasEntrance) {
                    if (nt.GetComponent<Node>().innerAreaObject.name == this.name && nt.GetComponent<Node>().innerEntranceObject.activeSelf)
                    {
                        nt.GetComponent<Node>().innerEntranceObject.SetActive(false);
                        nt.GetComponent<Node>().HasEntrance = false;
                    }
                    else if(nt.GetComponent<Node>().outerAreaObject.name == this.name && nt.GetComponent<Node>().outerEntranceObject.activeSelf)
                    {
                        nt.GetComponent<Node>().outerEntranceObject.SetActive(false);
                        nt.GetComponent<Node>().HasEntrance = false;
                    }
                }

            }
        }

        void OnMouseDown()
        {
            Debug.Log("mousedown");
            if (userInterface.GetComponent<InterfaceDisplay>().BuyDecision && userInterface.GetComponent<InterfaceDisplay>().BuyDecisionPressed)
            {
                if (GameObject.Find("System").GetComponent<GameFlow>().CurrPlayerObject.GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().innerAreaObject.name == this.name
                    || GameObject.Find("System").GetComponent<GameFlow>().CurrPlayerObject.GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().outerAreaObject.name == this.name)
                {
                    Debug.Log("mouseinsideBUY");
                    GameObject.Find("System").GetComponent<GameFlow>().AreaToBuy = this.area;
                    GameObject.Find("System").GetComponent<GameFlow>().areaSelected = true;
                    userInterface.GetComponent<InterfaceDisplay>().BuyDecision = false;
                    userInterface.GetComponent<InterfaceDisplay>().BuyDecisionPressed = false;
                }
                else
                {
                    Debug.Log("Not adjascent area");
                }
            }
            else if (userInterface.GetComponent<InterfaceDisplay>().BuildDecision && userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed)
            {
                Debug.Log("mouseinsideBUILD");
                GameObject.Find("System").GetComponent<GameFlow>().AreaToBuild = this.area;
                GameObject.Find("System").GetComponent<GameFlow>().areaSelected = true;
                userInterface.GetComponent<InterfaceDisplay>().BuildDecision = false;
                userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed = false;
            }
            else if (GameObject.Find("System").GetComponent<GameFlow>().TileBothAreas)
            {
                GameObject.Find("System").GetComponent<GameFlow>().AreaEntranceToBuild = this.area;
                GameObject.Find("System").GetComponent<GameFlow>().areaSelected = true;

            }
            
        }
    }
}