using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerInfo pdata;


        public PlayerInfo Pdata { get => pdata; set => pdata = value; }



        public void movePlayer(int dist)
        {
            
            
            for (int m = 0; m < dist; m++)
            {

                StartCoroutine(Run(5f, 3f));
            }
            
            
        }

        private IEnumerator Run(float duration, float waitTime)
        {
            //int index = 0;
            
            float time = 0;
            WaitForSeconds wait = new WaitForSeconds(waitTime);
            Vector3 startPosition = this.pdata.Figure.transform.position;
            Vector3 endPosition = getPosOnTile(this.pdata.CurrentPosTile.GetComponent<Node>().nextNode).position;

            for (time = 0; time < duration; time += Time.deltaTime){

                    this.pdata.Figure.transform.position = Vector3.Lerp(startPosition, endPosition, time);

            }
            //yield return new WaitUntil(() => startPosition == endPosition);
            this.pdata.Figure.transform.rotation = this.pdata.CurrentPosTile.GetComponent<Node>().nextNode.transform.rotation;
            this.pdata.Figure.transform.rotation *= Quaternion.Euler(0, -90, 0);
            this.pdata.PreviousPosTile = this.pdata.CurrentPosTile;
            this.pdata.CurrentPosTile = this.pdata.CurrentPosTile.GetComponent<Node>().nextNode;
            this.pdata.Figure.transform.position = endPosition;
            checkForIncome();
            checkForEntrance();
            yield return wait;
            
 
        }


        private Transform getPosOnTile(GameObject destTile)
        {
            if(destTile.GetComponent<Node>().PlayersOnTile == 0)
            {
                return destTile.GetComponent<Node>().positions[0].transform;
            }
            else if (destTile.GetComponent<Node>().PlayersOnTile == 1)
            {
                return destTile.GetComponent<Node>().positions[1].transform;
            }
            else if(destTile.GetComponent<Node>().PlayersOnTile == 2)
            {
                return destTile.GetComponent<Node>().positions[2].transform;
            }
            else if (destTile.GetComponent<Node>().PlayersOnTile == 3)
            {
                return destTile.GetComponent<Node>().positions[3].transform;
            }
            return null;
        }

        private void checkForIncome()
        {
            if(this.pdata.PreviousPosTile.GetComponent<Node>().ID1 == 9 && this.pdata.CurrentPosTile.GetComponent<Node>().ID1 == 10)
            {
                this.pdata.Cash += 3000;
                GameObject.Find("UserInterface").GetComponent<InterfaceDisplay>().updatePlayerStats();
            }
        }

        private void checkForEntrance()
        {
            Debug.Log("CHECK ENTRANCE");
            if (this.pdata.PreviousPosTile.GetComponent<Node>().ID1 == 17 && this.pdata.CurrentPosTile.GetComponent<Node>().ID1 == 18)
            {
                GameObject.Find("System").GetComponent<GameFlow>().State = GameFlow.turnState.BuyEntrance;
                GameObject.Find("System").GetComponent<GameFlow>().ActionStarted = false;
                GameObject.Find("System").GetComponent<GameFlow>().BuyEntranceCounter = 0;
            }
        }

    }


    
}