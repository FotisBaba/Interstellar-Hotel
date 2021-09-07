using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace gameNameSpace
{
    public class InterfaceDisplay : MonoBehaviour
    {
        public GameObject[] playerInfo;
        public GameObject[] areaInfo;
        public GameObject game;

        public GameObject gameCamera;
        public GameObject creditsCamera;
        public GameObject pauseCamera;
        public GameObject pauseInterface;


        private bool buyDecisionPressed = false;
        private bool buyDecision;
        private bool buyEntranceDecisionPressed = false;
        private bool buyEntranceDecision;
        private bool buildDecisionPressed = false;
        private bool buildDecision;
        private bool auctionDecisionPressed = false;
        private bool auctionDecision;
        private bool buildCountPressed = false;
        private bool endTurnPressed = false;
        private bool playAgainPressed = false;
        private int moveDieV;
        private bool moveDieReady;
        private BuildDieValues buildDieV;
        private bool buildDieReady;
        private int shuffleTimes, shuffleCounter;
        private bool creditsOn;

        public int MoveDieV { get => moveDieV; set => moveDieV = value; }
        public BuildDieValues BuildDieV { get => buildDieV; set => buildDieV = value; }
        public bool MoveDieReady { get => moveDieReady; set => moveDieReady = value; }
        public bool BuildDieReady { get => buildDieReady; set => buildDieReady = value; }
        public bool BuyDecisionPressed { get => buyDecisionPressed; set => buyDecisionPressed = value; }
        public bool BuyDecision { get => buyDecision; set => buyDecision = value; }
        public bool BuildDecisionPressed { get => buildDecisionPressed; set => buildDecisionPressed = value; }
        public bool BuildDecision { get => buildDecision; set => buildDecision = value; }
        public bool EndTurnPressed { get => endTurnPressed; set => endTurnPressed = value; }
        public bool PlayAgainPressed { get => playAgainPressed; set => playAgainPressed = value; }
        public bool BuildCountPressed { get => buildCountPressed; set => buildCountPressed = value; }
        public bool AuctionDecisionPressed { get => auctionDecisionPressed; set => auctionDecisionPressed = value; }
        public bool AuctionDecision { get => auctionDecision; set => auctionDecision = value; }
        public bool BuyEntranceDecisionPressed { get => buyEntranceDecisionPressed; set => buyEntranceDecisionPressed = value; }
        public bool BuyEntranceDecision { get => buyEntranceDecision; set => buyEntranceDecision = value; }
        public bool CreditsOn { get => creditsOn; set => creditsOn = value; }


        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {
            if (CreditsOn)
            {
                if (Input.GetKeyDown("space"))
                {
                    creditsCamera.SetActive(false);
                    GameObject.Find("CreditsCanvas").transform.GetChild(1).gameObject.SetActive(false);
                    game.GetComponent<GameFlow>().creditAnim.GetComponent<Animator>().enabled = false;
                    gameCamera.SetActive(true);
                }
            }

        }

        public void showMoveDieUI()
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(9).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        public void hideMoveDieUI()
        {
            this.transform.GetChild(9).gameObject.SetActive(false);
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            
        }

        public void showOvernightDieUI()
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(18).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(false);
            this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }

        public void hideOvernightDieUI()
        {
            this.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(18).gameObject.SetActive(false);
            this.transform.GetChild(2).gameObject.SetActive(false);

        }

        public void showBuildDieUI()
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(14).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }

        public void hideBuildDieUI()
        {
            this.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(14).gameObject.SetActive(false);
            //this.transform.GetChild(2).gameObject.SetActive(false);

        }

        public void showCantBuy()
        {
            this.transform.GetChild(7).gameObject.SetActive(true);
        }

        public void hideCantBuy()
        {
            this.transform.GetChild(7).gameObject.SetActive(false);
        }

        public void showCantBuild()
        {
            this.transform.GetChild(12).gameObject.SetActive(true);
        }

        public void hideCantBuild()
        {
            this.transform.GetChild(12).gameObject.SetActive(false);
        }

        public void showCantBuildCount()
        {
            this.transform.GetChild(13).gameObject.SetActive(true);
        }

        public void hideCantBuildCount()
        {
            this.transform.GetChild(13).gameObject.SetActive(false);
        }

        public void showBuyDecisionUI()
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
        }

        public void hideBuyDecisionUI()
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
        }

        public void showChooseAreaTobuy()
        {
            this.transform.GetChild(6).gameObject.SetActive(true);
        }

        public void hideChooseAreaTobuy()
        {
            this.transform.GetChild(6).gameObject.SetActive(false);
        }

        public void showChooseAreaToBuild()
        {
            this.transform.GetChild(11).gameObject.SetActive(true);
        }

        public void hideChooseAreaToBuild()
        {
            this.transform.GetChild(11).gameObject.SetActive(false);
        }
        public void showBuildCountDecision()
        {
            this.transform.GetChild(5).gameObject.SetActive(true);
        }

        public void hideBuildCountDecision()
        {
            this.transform.GetChild(5).gameObject.SetActive(false);
        }

        public void showBuildDecisionUI()
        {
            this.transform.GetChild(4).gameObject.SetActive(true);
        }

        public void hideBuildDecisionUI()
        {
            this.transform.GetChild(4).gameObject.SetActive(false);
        }

        public void showTravelAgainUI()
        {
            this.transform.GetChild(8).gameObject.SetActive(true);
        }

        public void hideTravelAgainUI()
        {
            this.transform.GetChild(8).gameObject.SetActive(false);
        }
        public void showTravelMsgnUI()
        {
            this.transform.GetChild(9).gameObject.SetActive(true);
        }

        public void hideTravelMsgUI()
        {
            this.transform.GetChild(9).gameObject.SetActive(false);
        }

        public void showEndTurnUI()
        {
            this.transform.GetChild(10).gameObject.SetActive(true);
        }

        public void hideEndTurnUI()
        {
            this.transform.GetChild(10).gameObject.SetActive(false);
        }

        public void showNoEntranceAvailableUI()
        {
            this.transform.GetChild(15).gameObject.SetActive(true);
        }

        public void hideNoEntranceAvailableUI()
        {
            this.transform.GetChild(15).gameObject.SetActive(false);
        }

        public void showChooseTileUI()
        {
            this.transform.GetChild(16).gameObject.SetActive(true);
        }

        public void hideChooseTileUI()
        {
            this.transform.GetChild(16).gameObject.SetActive(false);
        }

        public void showChooseAreaOfEntranceUI()
        {
            this.transform.GetChild(17).gameObject.SetActive(true);
        }

        public void hideChooseAreaOfEntranceUI()
        {
            this.transform.GetChild(17).gameObject.SetActive(false);
        }

        public void showBuyEntranceUI()
        {
            this.transform.GetChild(20).gameObject.SetActive(true);
        }

        public void hideBuyEntranceUI()
        {
            this.transform.GetChild(20).gameObject.SetActive(false);
        }
        
        public void hideNewGameMenu()
        {
            GameObject.Find("StartingCanvas").transform.GetChild(4).gameObject.SetActive(false);

        }

        public void updatePlayerStats()
        {
            if (game.GetComponent<GameFlow>().GameOngoing)
            {
                
                for (int i = 0; i < game.GetComponent<GameFlow>().NumOfPlayers; i++)
                {
                    if (!game.GetComponent<GameFlow>().PlayerObjs[i].GetComponent<Player>().Pdata.HasLost)
                    {
                        playerInfo[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = game.GetComponent<GameFlow>().PlayerObjs[i].GetComponent<Player>().Pdata.PlayerName;
                        playerInfo[i].transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = game.GetComponent<GameFlow>().PlayerObjs[i].GetComponent<Player>().Pdata.Cash.ToString();
                    }
                }

            }
        }

        public void updateAreaStats()
        {
            GameObject parent = GameObject.Find("Areas");
            GameObject UIparent = GameObject.Find("WindowArea");
            for (int i = 0; i < 9; i++)
            {
                GameObject areaObj = parent.transform.GetChild(i).gameObject;
                GameObject UIObj = UIparent.transform.GetChild(i).gameObject;
                UIObj.GetComponent<TMP_Text>().text = areaObj.name + " owner is " + areaObj.GetComponent<Area>().OwnerName + "\nBuildings: " + areaObj.GetComponent<Area>().Builds;

            }

            
        }

        public void rolledMoveDie()
        {
            moveDieReady = false;
            shuffleTimes = Random.Range(4, 11);
            
            shuffleCounter = 0;
            Invoke("realMoveRoll", 0.1f);


        }

        private void realMoveRoll()
        {
            MoveDieV = Random.Range(1, 7);
            GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "You rolled " + MoveDieV.ToString();
            shuffleCounter++;
            if (shuffleCounter < shuffleTimes)
            {
                Invoke("realMoveRoll", 0.1f);
            }
            else
            {
                moveDieReady = true;
            }

        }

        public void rolledBuildDie()
        {
            buildDieReady = false;
            shuffleTimes = Random.Range(4, 11);

            shuffleCounter = 0;
            Invoke("realBuildRoll", 0.1f);


        }

        private void realBuildRoll()
        {
            int res = Random.Range(1, 7);
            //GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "You rolled " + MoveDieV.ToString();
            
            if(res <= 3)
            {
                buildDieV = BuildDieValues.Success;
                GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "Success!";
            }
            else if (res <= 4)
            {
                buildDieV = BuildDieValues.Fail;
                GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "<color=orange>Fail!</color>";
            }
            else if (res <= 5)
            {
                buildDieV = BuildDieValues.Double;
                GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "<color=red>Double cost!</color>";
            }
            else
            {
                buildDieV = BuildDieValues.Free;
                GameObject.Find("DieResultText").GetComponent<TMP_Text>().text = "<color=green>Free Build!</color>";
            }
            
            shuffleCounter++;
            if (shuffleCounter < shuffleTimes)
            {
                Invoke("realBuildRoll", 0.1f);
            }
            else
            {
                buildDieReady = true;
            }

        }

        public void hideAfterTurn()
        {
            hideMoveDieUI();
            hideBuildDieUI();
            hideCantBuy();
            hideCantBuild();
            hideCantBuildCount();
            hideBuyDecisionUI();
            hideChooseAreaTobuy();
            hideChooseAreaToBuild();
            hideBuildCountDecision();
            hideBuildDecisionUI();
            hideTravelAgainUI();
            hideEndTurnUI();
            hideNoEntranceAvailableUI();
            hideChooseTileUI();
            hideChooseAreaOfEntranceUI();
        }

        public enum BuildDieValues
        {
            Success, Fail, Double, Free
        }

        public void yesBuy()
        {
            buyDecisionPressed = true;
            buyDecision = true;
            game.GetComponent<GameFlow>().ActionDone = false;
            game.GetComponent<GameFlow>().PurchaseComplete = false;
            //this.transform.GetChild(6).gameObject.SetActive(true);
        }
        
        public void noBuy()
        {
            buyDecisionPressed = true;
            buyDecision = false;
            game.GetComponent<GameFlow>().ActionDone = true;
        }
        public void yesBuyEntrance()
        {
            buyEntranceDecisionPressed = true;
            buyEntranceDecision = true;
            game.GetComponent<GameFlow>().ActionDone = false;
            game.GetComponent<GameFlow>().PurchaseComplete = false;
            //this.transform.GetChild(6).gameObject.SetActive(true);
        }

        public void noBuyEntrance()
        {
            buyEntranceDecisionPressed = true;
            buyEntranceDecision = false;
            //game.GetComponent<GameFlow>().ActionDone = true;
        }


        public void endTurn()
        {
            endTurnPressed = true;
        }

        public void yesBuild()
        {
            buildDecisionPressed = true;
            buildDecision = true;
            game.GetComponent<GameFlow>().ActionDone = false;
            game.GetComponent<GameFlow>().PurchaseComplete = false;
        }

        public void noBuild()
        {
            buildDecisionPressed = true;
            buildDecision = false;
            game.GetComponent<GameFlow>().ActionDone = true;
            
        }

        public void yesAuction()
        {
            AuctionDecisionPressed = true;
            AuctionDecision = true;
        }

        public void noAuction()
        {
            AuctionDecisionPressed = true;
            AuctionDecision = false;
        }

        public void buildOne()
        {
            buildCountPressed = true;
            game.GetComponent<GameFlow>().BuildCount = 1;
        }

        public void buildTwo()
        {
            buildCountPressed = true;
            game.GetComponent<GameFlow>().BuildCount = 2;
        }

        public void buildThree()
        {
            buildCountPressed = true;
            game.GetComponent<GameFlow>().BuildCount = 3;

        }

        public void playAgainYes()
        {
            playAgainPressed = true;
            game.GetComponent<GameFlow>().PlayAgainChoice = true;
        }

        public void playAgainNo()
        {
            playAgainPressed = true;
            game.GetComponent<GameFlow>().PlayAgainChoice = false;
        }

        public void playerLostUI(string pName)
        {
            GameObject panel = GameObject.Find("PanelArea");
            for (int i = 0; i < game.GetComponent<GameFlow>().NumOfPlayers; i++) {
                if (panel.transform.GetChild(i).GetChild(0).gameObject.GetComponent<TMP_Text>().text == pName)
                {
                    panel.transform.GetChild(i).GetChild(2).gameObject.SetActive(false);
                    panel.transform.GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "lost!";
                }
            }
        }

        public void resumeGame()
        {
            pauseCamera.SetActive(false);
            gameCamera.SetActive(true);
            pauseInterface.SetActive(false);
        }

        public void toMainMenu()
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }

        public void noMainMenu()
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        
        public void yesMainMenu()
        {
            for(int i = 0; i < game.GetComponent<GameFlow>().NumOfPlayers; i++)
            {
                game.GetComponent<GameFlow>().PlayerObjs[i].GetComponent<Player>().Pdata.Figure.transform.SetParent(GameObject.Find("AvailableFigures").transform);
            }
            int nm = Object.FindObjectsOfType<DontDestroy>().Length;
            for (int i = nm; i >0; i--)
            {
              
                    Destroy(Object.FindObjectsOfType<DontDestroy>()[i-1].gameObject);
            }
            
            game.GetComponent<GameFlow>().PlayerObjs = null;
            SceneManager.LoadScene(0, LoadSceneMode.Single);
            
            
        }

        public void pauseGame()
        {
            Debug.Log("pause");   
            
            gameCamera.SetActive(false);
            pauseCamera.SetActive(true);
            pauseInterface.SetActive(true);
        }

        public void gameOver()
        {
            this.transform.GetChild(22).gameObject.SetActive(true);
        }

        public void hidePlayerInfo()
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        public void hideBoardInfo()
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        public void hideDiceInfo()
        {
            this.transform.GetChild(2).gameObject.SetActive(false);
        }


    }
}
