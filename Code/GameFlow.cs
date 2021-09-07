using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gameNameSpace
{
    public class GameFlow : MonoBehaviour
    {
        
        private int numOfPlayers;
        [SerializeField] public GameObject newGameMenu;
        [SerializeField] private GameObject[] textForPlayerNames;
        public GameObject playerPrefab;
        [SerializeField] private GameObject[] playerObjs;
        public Transform playerParent;
        //public List<GameObject> players;
        public GameObject userInterface;
        public GameObject creditsInterface;
        private GameObject currPlayerObject;
        [SerializeField]
        private GameObject[] playersPanel;
        private Player tempPlayer;
        private bool _gameOngoing = false;
        private GameObject areaToBuy = null;
        private GameObject areaToBuild;
        private GameObject entranceTileToBuild;
        private GameObject areaEntranceToBuild;
        private GameObject overnightArea;
        GameObject playerGettingPaid;
        private int buildCount;
        public bool areaSelected = false;
        public bool tileSelected = false;
        public bool waitingForTile = false;
        private bool purchaseComplete = false;
        private bool buildComplete = false;
        private int buildCost = 0;
        private bool canPlayAgain = false;
        private bool playAgainChoice = false;
        private bool actionStarted = false;
        private bool actionDone = false;
        private bool tileBothAreas = false;
        private int buyEntranceCounter = 0;
        [SerializeField] private GameObject buildDie;
        [SerializeField]  private GameObject moveDie;

        [SerializeField] public GameObject nopText;
        public GameObject mainMenuCamera;
        public GameObject figureSelectCamera;
        public GameObject pauseCamera;
        public GameObject creditsCamera;
        public Animator creditAnim;
        [SerializeField]  public AudioSource exchangeComplete;

        public GameObject[] plFigureSelectText;
        public GameObject startMsg;
        

        private turnState state;
        private buyState buyS;
        private buildState buildS;
        private freeBuildState fBS;
        private freeEntranceState fES;
        private buyEntranceState bES;
        private payState payS;

        public GameObject buttonToclose;

        public Transform travelParticle1;
        public Transform travelParticle2;

        


        public enum turnState
        {
            RollMoveDie, BuyEntrance, PayAction, NodeAction, EndTurn, NextPlayer
        }

        public enum buyState
        {
            BeginAction, WaitDecision, ChooseArea, EndTransaction
        }

        public enum buildState
        {
            BeginAction, WaitDecision, ChooseArea, WaitBuildCount, RollBuildDie, EndTransaction
        }

        public enum freeBuildState
        {
            BeginAction, ChooseArea, EndTransaction
        }

        public enum freeEntranceState
        {
            BeginAction, ChooseTile, EndTransaction
        }

        public enum buyEntranceState
        {
            BeginAction, ChooseTiles, EndTransaction
        }

        public enum payState
        {
            BeginAction, WaitOvernight, EndTransaction
        }


        public GameObject[] PlayerObjs { get => PlayerObjs1; set => PlayerObjs1 = value; }
        public global::System.Int32 NumOfPlayers { get => numOfPlayers; set => numOfPlayers = value; }
        public global::System.Boolean GameOngoing { get => _gameOngoing; set => _gameOngoing = value; }
        
        public GameObject AreaToBuy { get => areaToBuy; set => areaToBuy = value; }
        public GameObject AreaToBuild { get => areaToBuild; set => areaToBuild = value; }
        public GameObject CurrPlayerObject { get => currPlayerObject; set => currPlayerObject = value; }
        public bool PlayAgainChoice { get => playAgainChoice; set => playAgainChoice = value; }
        public bool ActionDone { get => actionDone; set => actionDone = value; }
        public bool PurchaseComplete { get => purchaseComplete; set => purchaseComplete = value; }
        public bool BuildComplete { get => buildComplete; set => buildComplete = value; }
        public int BuildCount { get => buildCount; set => buildCount = value; }
        public GameObject EntranceTileToBuild { get => entranceTileToBuild; set => entranceTileToBuild = value; }
        public bool TileBothAreas { get => tileBothAreas; set => tileBothAreas = value; }
        public GameObject AreaEntranceToBuild { get => areaEntranceToBuild; set => areaEntranceToBuild = value; }
        public turnState State { get => state; set => state = value; }
        public bool ActionStarted { get => actionStarted; set => actionStarted = value; }
        public int BuyEntranceCounter { get => buyEntranceCounter; set => buyEntranceCounter = value; }
        public GameObject[] PlayerObjs1 { get => playerObjs; set => playerObjs = value; }
        public GameObject[] PlayersPanel { get => playersPanel; set => playersPanel = value; }


        //public GameObject[] TextForPlayerNames { get => textForPlayerNames; set => textForPlayerNames = value; }
        //public GameObject[] PlFigureSelectText { get => plFigureSelectText; set => plFigureSelectText = value; }

        int i = 0;
        GameObject player;

        GameObject playerPos;




        // Start is called before the first frame update
        void Start()
        {
            if (!GameOngoing)
            {
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    GameObject.Find("System").GetComponent<GameFlow>().NumOfPlayers = Object.FindObjectsOfType<DontDestroy>().Length;
                    GameObject.Find("System").GetComponent<GameFlow>().PlayerObjs1 = new GameObject[Object.FindObjectsOfType<DontDestroy>().Length];
                    //Debug.Log("System is at position: " + GameObject.Find("System").transform.position);
                    Debug.Log("sceneloaded");
                    for (int j = 0; j < NumOfPlayers; j++)
                    {
                        //int pos = 0;
                        PlayerObjs[j] = (GameObject)Instantiate(GameObject.Find("System").GetComponent<GameFlow>().playerPrefab, GameObject.Find("System").GetComponent<GameFlow>().playerParent);
                        PlayerObjs[j].name = Object.FindObjectsOfType<DontDestroy>()[NumOfPlayers - 1 - j].gameObject.name;
                        PlayerObjs[j].GetComponent<Player>().Pdata = Object.FindObjectsOfType<DontDestroy>()[NumOfPlayers - 1 - j].gameObject.GetComponent<Player>().Pdata;
                        PlayersPanel[j].SetActive(true);
                        for (int k = 0; k < GameObject.Find("AvailableFigures").transform.childCount; k++)
                        {
                            //if (k < GameObject.Find("AvailableFigures").transform.childCount)
                            //{
                                if (PlayerObjs[j].GetComponent<Player>().Pdata.FigureName == GameObject.Find("AvailableFigures").transform.GetChild(k).gameObject.name)
                                {
                                    Debug.Log("FIGURE");
                                    PlayerObjs[j].GetComponent<Player>().Pdata.Figure = GameObject.Find("AvailableFigures").transform.GetChild(k).gameObject;
                                    GameObject.Find("AvailableFigures").transform.GetChild(k).SetParent(PlayerObjs[j].transform);
                                    PlayerObjs[j].GetComponent<Player>().Pdata.Figure.transform.position = GameObject.Find("Node1").transform.GetChild(2).transform.GetChild(j).transform.position;
                                    GameObject.Find("PanelArea").transform.GetChild(j).gameObject.SetActive(true);
                                    PlayerObjs[j].GetComponent<Player>().Pdata.CurrentPosTile = GameObject.Find("Node1");
                                }
                            //}
                        }

                    }

                    GameObject.Find("Players").transform.SetParent(GameObject.Find("System").transform);
                    GameOngoing = true;
                    State = turnState.RollMoveDie;
                    player = PlayerObjs[i];
                    currPlayerObject = player;
                    playerPos = PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile;
                    userInterface.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(true);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {


                if (checkGameEnded())
                {
                    userInterface.GetComponent<InterfaceDisplay>().gameOver();
                    GameOngoing = false;
                    for (int i = 0; i < userInterface.transform.childCount; i++)
                    {
                        if (i != 22)
                        {
                            userInterface.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    //userInterface.GetComponent<InterfaceDisplay>().hidePlayerInfo();
                    //userInterface.GetComponent<InterfaceDisplay>().hideBoardInfo();
                    //userInterface.GetComponent<InterfaceDisplay>().hideDiceInfo();


                }
                else if (GameOngoing)
                {
                    Debug.Log("ongoing");
                    userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
                    userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
                    if (State == turnState.RollMoveDie)
                    {

                        userInterface.GetComponent<InterfaceDisplay>().showMoveDieUI();
                        userInterface.GetComponent<InterfaceDisplay>().hideTravelAgainUI();
                        StartCoroutine(waitforMoveDie(player));

                        areaToBuild = null;
                        areaToBuy = null;
                        ActionStarted = false;
                    }
                    else if (State == turnState.BuyEntrance)
                    {
                        if (!ActionStarted)
                        {
                            bES = buyEntranceState.BeginAction;
                            ActionStarted = true;
                            userInterface.GetComponent<InterfaceDisplay>().hideMoveDieUI();
                            //TileBothAreas = false;
                        }

                        if (bES == buyEntranceState.BeginAction)
                        {

                            if (entranceAvailable(player))
                            {
                                waitingForTile = true;
                                bES = buyEntranceState.ChooseTiles;
                            }
                            else
                            {
                                userInterface.GetComponent<InterfaceDisplay>().showNoEntranceAvailableUI();
                                bES = buyEntranceState.EndTransaction;
                            }
                        }
                        else if (bES == buyEntranceState.ChooseTiles)
                        {
                            //userInterface.GetComponent<InterfaceDisplay>().showChooseTileUI();
                            //showAvailableEntrances(player);
                            //while (buyEntranceCounter < 9)
                            //Debug.Log(BuyEntranceCounter);
                            if (BuyEntranceCounter < 9)
                            {
                                if (GameObject.Find("Areas").transform.GetChild(BuyEntranceCounter).gameObject.GetComponent<Area>().OwnerName == player.name
                                    && GameObject.Find("Areas").transform.GetChild(BuyEntranceCounter).gameObject.GetComponent<Area>().Builds > 0)
                                {
                                    userInterface.transform.GetChild(20).GetChild(1).gameObject.GetComponent<TMPro.TMP_Text>().text = "Buy entrance for "
                                        + GameObject.Find("Areas").transform.GetChild(BuyEntranceCounter).gameObject.name + "?";
                                    userInterface.GetComponent<InterfaceDisplay>().showBuyEntranceUI();
                                    StartCoroutine(buyEntrancePoint(player));
                                }
                                else
                                {

                                    BuyEntranceCounter++;
                                    //Debug.Log(BuyEntranceCounter);
                                    if (BuyEntranceCounter == 9)
                                    {
                                        BuyEntranceCounter = 0;
                                        waitingForTile = false;
                                        bES = buyEntranceState.EndTransaction;
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("CHILD OUT OF BOUDNS BUY ENTRANCE");
                                bES = buyEntranceState.EndTransaction;
                            }


                            //}
                        }
                        else if (bES == buyEntranceState.EndTransaction)
                        {
                            userInterface.GetComponent<InterfaceDisplay>().hideBuyEntranceUI();
                            userInterface.GetComponent<InterfaceDisplay>().hideChooseTileUI();
                            userInterface.GetComponent<InterfaceDisplay>().hideNoEntranceAvailableUI();
                            showAllTiles();
                            BuyEntranceCounter = 0;
                            State = turnState.PayAction;
                        }


                    }
                    else if (State == turnState.PayAction)
                    {

                        if (!ActionStarted)
                        {
                            payS = payState.BeginAction;
                            ActionStarted = true;
                            //userInterface.GetComponent<InterfaceDisplay>().hideMoveDieUI();

                        }

                        if (payS == payState.BeginAction)
                        {
                            if (playerPos.GetComponent<Node>().HasEntrance)
                            {
                                Debug.Log("PAY ACTION/n");
                                Debug.Log("HAS ENTRANCE");
                                if (playerPos.GetComponent<Node>().innerEntranceObject.activeSelf)
                                {

                                    if (playerPos.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != player.name)
                                    {
                                        for (int p = 0; p < numOfPlayers; p++)
                                        {
                                            if (PlayerObjs1[p].name == playerPos.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName)
                                            {
                                                playerGettingPaid = PlayerObjs1[p];
                                                overnightArea = playerPos.GetComponent<Node>().innerAreaObject;
                                                payS = payState.WaitOvernight;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        payS = payState.EndTransaction;
                                    }
                                }
                                else if (playerPos.GetComponent<Node>().outerEntranceObject.activeSelf)
                                {
                                    if (playerPos.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != player.name)
                                    {
                                        for (int p = 0; p < numOfPlayers; p++)
                                        {
                                            if (PlayerObjs1[p].name == playerPos.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName)
                                            {
                                                playerGettingPaid = PlayerObjs1[p];
                                                overnightArea = playerPos.GetComponent<Node>().outerAreaObject;
                                                payS = payState.WaitOvernight;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        payS = payState.EndTransaction;
                                    }
                                }
                            }
                            else
                            {
                                payS = payState.EndTransaction;
                            }

                        }
                        else if (payS == payState.WaitOvernight)
                        {
                            Debug.Log("WAIT OVERNIGHT");
                            userInterface.GetComponent<InterfaceDisplay>().hideTravelMsgUI();
                            userInterface.GetComponent<InterfaceDisplay>().showOvernightDieUI();
                            StartCoroutine(waitforOvernights(player, playerGettingPaid, overnightArea));
                        }
                        else if (payS == payState.EndTransaction)
                        {
                            Debug.Log("PAY ACTION END TRANSACTION");
                            userInterface.GetComponent<InterfaceDisplay>().hideBuyEntranceUI();
                            showAllTiles();
                            BuyEntranceCounter = 0;
                            payS = payState.BeginAction;
                            ActionStarted = false;
                            State = turnState.NodeAction;
                        }





                    }
                    else if (State == turnState.NodeAction)
                    {
                        userInterface.GetComponent<InterfaceDisplay>().hideMoveDieUI();
                        if (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.BuyNode)
                        {
                            if (!ActionStarted)
                            {
                                buyS = buyState.BeginAction;
                                ActionStarted = true;
                            }


                            if (buyS == buyState.BeginAction)
                            {
                                Debug.Log("begin ACTION");
                                if (buyAvailable(player, playerPos))
                                {
                                    Debug.Log("buyAvailable");
                                    buyS = buyState.WaitDecision;
                                }
                                else
                                {
                                    Debug.Log("Cant BUY");
                                    userInterface.GetComponent<InterfaceDisplay>().showCantBuy();
                                    buyS = buyState.EndTransaction;
                                }
                            }
                            else if (buyS == buyState.WaitDecision)
                            {
                                Debug.Log("WAIT DECISION");
                                userInterface.GetComponent<InterfaceDisplay>().showBuyDecisionUI();
                                StartCoroutine(waitBuyDecision());

                            }
                            else if (buyS == buyState.ChooseArea)
                            {
                                //areaToBuy = null;
                                Debug.Log("CHOOSE AREA");
                                showAvailablePurchaces(player, playerPos);
                                userInterface.GetComponent<InterfaceDisplay>().showChooseAreaTobuy();
                                StartCoroutine(waitForAreaToBuy(player));

                            }
                            else if (buyS == buyState.EndTransaction)
                            {

                                Debug.Log("END TRANSACTION");
                                if (canPlayAgain)
                                {
                                    Debug.Log("Cant BUY pLAY AGAIN");
                                    userInterface.GetComponent<InterfaceDisplay>().showTravelAgainUI();
                                    StartCoroutine(waitTravelAgain());
                                }
                                else
                                {
                                    Debug.Log("CANT PLAY AGAIN");
                                    userInterface.GetComponent<InterfaceDisplay>().showEndTurnUI();
                                    StartCoroutine(waitEndTurn());
                                }

                            }
                        }
                        else if (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.BuildNode)
                        {
                            if (!ActionStarted)
                            {
                                buildS = buildState.BeginAction;
                                ActionStarted = true;
                                //BuildComplete = false;
                            }


                            if (buildS == buildState.BeginAction)
                            {
                                Debug.Log("begin ACTION");
                                if (buildAvailable(player))
                                {
                                    Debug.Log("buildAvailable");
                                    buildS = buildState.WaitDecision;


                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showCantBuild();
                                    buildS = buildState.EndTransaction;
                                }
                            }
                            else if (buildS == buildState.WaitDecision)
                            {
                                Debug.Log("WAIT DECISION");
                                userInterface.GetComponent<InterfaceDisplay>().showBuildDecisionUI();
                                StartCoroutine(waitBuildDecision());

                            }
                            else if (buildS == buildState.ChooseArea)
                            {

                                Debug.Log("CHOOSE AREA");
                                showAvailableBuilds(player);
                                userInterface.GetComponent<InterfaceDisplay>().showChooseAreaToBuild();
                                StartCoroutine(waitForAreaToBuild(player));

                            }
                            else if (buildS == buildState.WaitBuildCount)
                            {
                                Debug.Log("CHOOSE BUILD COUNT");
                                //showAvailableBuilds(player);
                                userInterface.GetComponent<InterfaceDisplay>().showBuildCountDecision();
                                StartCoroutine(waitForBuildCount(player));

                            }
                            else if (buildS == buildState.RollBuildDie)
                            {
                                Debug.Log("ROLL BUILD DIE");
                                int cost = calculateBuildCost();
                                userInterface.GetComponent<InterfaceDisplay>().showBuildDieUI();
                                StartCoroutine(waitForBuildDie(player, cost));

                            }
                            else if (buildS == buildState.EndTransaction)
                            {

                                Debug.Log("END TRANSACTION");
                                if (canPlayAgain)
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showTravelAgainUI();
                                    StartCoroutine(waitTravelAgain());
                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showEndTurnUI();
                                    StartCoroutine(waitEndTurn());
                                }

                            }
                        }
                        else if (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.FreeBuildNode)
                        {
                            if (!ActionStarted)
                            {
                                fBS = freeBuildState.BeginAction;
                                ActionStarted = true;

                            }


                            if (fBS == freeBuildState.BeginAction)
                            {
                                //Debug.Log("begin ACTION");
                                if (buildAvailable(player))
                                {
                                    //Debug.Log("buildAvailable");
                                    userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed = true;
                                    userInterface.GetComponent<InterfaceDisplay>().BuildDecision = true;
                                    fBS = freeBuildState.ChooseArea;
                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showCantBuild();
                                    fBS = freeBuildState.EndTransaction;
                                }
                            }
                            else if (fBS == freeBuildState.ChooseArea)
                            {

                                //Debug.Log("CHOOSE AREA");
                                showAvailableBuilds(player);
                                userInterface.GetComponent<InterfaceDisplay>().showChooseAreaToBuild();

                                StartCoroutine(waitForAreaToBuild(player));

                            }
                            else if (fBS == freeBuildState.EndTransaction)
                            {

                                //Debug.Log("END TRANSACTION");
                                if (canPlayAgain)
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showTravelAgainUI();
                                    StartCoroutine(waitTravelAgain());
                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showEndTurnUI();
                                    StartCoroutine(waitEndTurn());
                                }

                            }
                        }
                        else if (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.FreeEntranceNode)
                        {

                            if (!ActionStarted)
                            {
                                fES = freeEntranceState.BeginAction;
                                ActionStarted = true;
                                TileBothAreas = false;
                            }

                            if (fES == freeEntranceState.BeginAction)
                            {

                                if (entranceAvailable(player))
                                {
                                    
                                    fES = freeEntranceState.ChooseTile;
                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showNoEntranceAvailableUI();
                                    fES = freeEntranceState.EndTransaction;
                                }
                            }
                            else if (fES == freeEntranceState.ChooseTile)
                            {
                                userInterface.GetComponent<InterfaceDisplay>().showChooseTileUI();
                                waitingForTile = true;
                                showAvailableEntrances(player);
                                StartCoroutine(waitForTileEntrance(player));
                            }
                            else if (fES == freeEntranceState.EndTransaction)
                            {
                                showAllTiles();
                                userInterface.GetComponent<InterfaceDisplay>().hideChooseTileUI();
                                if (canPlayAgain)
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showTravelAgainUI();
                                    StartCoroutine(waitTravelAgain());
                                }
                                else
                                {
                                    userInterface.GetComponent<InterfaceDisplay>().showEndTurnUI();
                                    StartCoroutine(waitEndTurn());
                                }
                            }
                        }




                    }
                    else if (State == turnState.EndTurn)
                    {
                        userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
                        userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
                        State = turnState.NextPlayer;
                    }
                    else if (State == turnState.NextPlayer)
                    {
                        //Debug.Log("NEXT PLAYER");
                        if (i + 1 >= numOfPlayers)
                        {
                            i = 0;
                        }
                        else
                        {
                            i++;
                        }

                        if (!PlayerObjs[i].GetComponent<Player>().Pdata.HasLost)
                        {
                            player = PlayerObjs[i];
                            currPlayerObject = player;
                            ActionStarted = false;
                            State = turnState.RollMoveDie;
                            userInterface.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i).GetChild(3).gameObject.SetActive(true);
                            if (i - 1 >= 0)
                            {
                                userInterface.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(i - 1).GetChild(3).gameObject.SetActive(false);
                            }
                            else
                            {
                                userInterface.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(numOfPlayers - 1).GetChild(3).gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            State = turnState.NextPlayer;
                        }
                    }
                }


            }







        }

        



        public bool buyAvailable(GameObject player, GameObject position)
        {
            if (position.GetComponent<Node>().innerAreaObject != null
                && position.GetComponent<Node>().outerAreaObject != null)
            {
                if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != "None"
                && position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != "None")
                {
                    if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != player.name
                        && position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != player.name)
                    {
                        if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().Builds > 0
                            && position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().Builds > 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (player.GetComponent<Player>().Pdata.Cash >= 500)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName == "None"
                    || position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName == "None")
                {
                    if (player.GetComponent<Player>().Pdata.Cash >= 1000)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if(position.GetComponent<Node>().innerAreaObject != null
                    && position.GetComponent<Node>().outerAreaObject == null)
            {
                if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != "None")
                {
                    if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != player.name)
                    {
                        if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().Builds > 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (player.GetComponent<Player>().Pdata.Cash >= 500)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName == "None")
                {
                    if (player.GetComponent<Player>().Pdata.Cash >= 1000)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (position.GetComponent<Node>().innerAreaObject == null
                   && position.GetComponent<Node>().outerAreaObject != null)
            {
                if (position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != "None")
                {
                    if (position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != player.name)
                    {
                        if (position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().Builds > 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (player.GetComponent<Player>().Pdata.Cash >= 500)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName == "None")
                {
                    if (player.GetComponent<Player>().Pdata.Cash >= 1000)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool buildAvailable(GameObject player)
        {
            for(int a = 0; a < 9; a++)
            {
                if(GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().OwnerName == player.name 
                    && GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().Builds <3)
                {
                    return true;
                }
            }
            return false;
        }

        public void showAvailableBuilds(GameObject player)
        {
            for (int a = 0; a < 9; a++)
            {
                GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(false);
                if (GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().OwnerName == player.name
                    && GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().Builds < 3)
                {
                    GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                }
            }
            
        }

        public void showAvailablePurchaces(GameObject player, GameObject position)
        {
            for (int a = 0; a < 9; a++)
            {
                GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(false);
                
                    if (position.GetComponent<Node>().innerAreaObject != null)
                    {
                        if (GameObject.Find("Areas").transform.GetChild(a).gameObject.name == position.GetComponent<Node>().innerAreaObject.name)
                        {
                            if (position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName != player.name
                                && position.GetComponent<Node>().innerAreaObject.GetComponent<Area>().Builds == 0)
                            {
                            //Debug.Log(player.name);
                            //Debug.Log(position.GetComponent<Node>().innerAreaObject.name);
                            
                            //Debug.Log(position.name);
                            GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                            }
                        }
                    }

                    if (position.GetComponent<Node>().outerAreaObject != null)
                    {
                        if (GameObject.Find("Areas").transform.GetChild(a).gameObject.name == position.GetComponent<Node>().outerAreaObject.name)
                        {
                            if (position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName != player.name
                            && position.GetComponent<Node>().outerAreaObject.GetComponent<Area>().Builds == 0)
                            {
                            //Debug.Log(player.name);
                            //Debug.Log(position.GetComponent<Node>().outerAreaObject.name);
                            //Debug.Log(position.name);
                            GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);

                            }
                        }
                    }

                

            }
          
        }

        public IEnumerator waitforMoveDie(GameObject player)
        {
            if(state == turnState.RollMoveDie) { 
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().MoveDieReady == true);
                if (userInterface.GetComponent<InterfaceDisplay>().MoveDieReady)
                {
                    
                    startMsg.SetActive(false);
                    PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().PlayersOnTile = 0;
                    travelParticle1.position = player.GetComponent<Player>().Pdata.Figure.transform.position;
                    travelParticle1.GetComponent<ParticleSystem>().Play();
                    player.GetComponent<Player>().movePlayer(userInterface.GetComponent<InterfaceDisplay>().MoveDieV);

                    travelParticle2.position = player.GetComponent<Player>().Pdata.Figure.transform.position;
                    travelParticle2.GetComponent<ParticleSystem>().Play();
                    if (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().PlayersOnTile == 0)
                    {
                        playerPos = PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile;
                        playerPos.GetComponent<Node>().PlayersOnTile = 1;
                        ActionStarted = false;
                        userInterface.GetComponent<InterfaceDisplay>().MoveDieReady = false;
                    }
                    else
                    {
                        userInterface.GetComponent<InterfaceDisplay>().MoveDieV = 1;
                        while (PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().PlayersOnTile != 0)
                        {
                            player.GetComponent<Player>().movePlayer(userInterface.GetComponent<InterfaceDisplay>().MoveDieV);
                        }
                        
                    }



                    if (state != turnState.BuyEntrance)
                    {
                        state = turnState.PayAction;
                    }
                    //userInterface.GetComponent<InterfaceDisplay>().showMoveDieUI();
                    //userInterface.GetComponent<InterfaceDisplay>()
                    //userInterface.GetComponent<InterfaceDisplay>().
                    
                    
                    
                    //playerPos = PlayerObjs[i].GetComponent<Player>().Pdata.CurrentPosTile;
                    if (userInterface.GetComponent<InterfaceDisplay>().MoveDieV == 6)
                    {
                        canPlayAgain = true;
                    }
                    else
                    {
                        canPlayAgain = false;
                    }
                }

            }
        }

        public IEnumerator waitforOvernights(GameObject player, GameObject pgettingPaid, GameObject ovnArea)
        {
            Debug.Log("WAIT OVERNIGHT DIE");
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().MoveDieReady == true);
            if (userInterface.GetComponent<InterfaceDisplay>().MoveDieReady)
            {
                Debug.Log("MOVE DIE READY");
                userInterface.GetComponent<InterfaceDisplay>().hideOvernightDieUI();
                int overnightCost = userInterface.GetComponent<InterfaceDisplay>().MoveDieV * ovnArea.GetComponent<Area>().Builds * 80;
                if (checkPlayerMoneyOK(player, overnightCost))
                {
                    
                    player.GetComponent<Player>().Pdata.Cash -= overnightCost;
                    Debug.Log(player.GetComponent<Player>().Pdata.Cash);
                    Debug.Log(overnightCost);
                    pgettingPaid.GetComponent<Player>().Pdata.Cash += overnightCost;
                    Debug.Log(pgettingPaid.GetComponent<Player>().Pdata.Cash);
                    userInterface.GetComponent<InterfaceDisplay>().MoveDieReady = false;
                    userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
                    userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
                    payS = payState.EndTransaction;
                }
                else
                {
                    Debug.Log("MONEY NOT OK");
                    playerLost(player); 
                                        
                }

            }
        }

        private bool checkPlayerMoneyOK(GameObject player, int cost)
        {

            if (player.GetComponent<Player>().Pdata.Cash < cost)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void playerLost(GameObject player)
        {            
            player.GetComponent<Player>().Pdata.HasLost = true;
            player.GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().PlayersOnTile = 0;
            player.GetComponent<Player>().Pdata.Figure.SetActive(false);
            for (int a = 0; a < 9; a++)
            {
                GameObject area = GameObject.Find("Areas").transform.GetChild(a).gameObject;
                if (area.GetComponent<Area>().OwnerName == player.name)
                {
                    area.GetComponent<Area>().ownerLost();
                }
            }
            userInterface.GetComponent<InterfaceDisplay>().playerLostUI(player.name);
            if (checkGameEnded())
            {
                GameOngoing = false;
                userInterface.GetComponent<InterfaceDisplay>().gameOver();
                userInterface.GetComponent<InterfaceDisplay>().hidePlayerInfo();
                userInterface.GetComponent<InterfaceDisplay>().hideBoardInfo();
            }
            else
            {
                state = turnState.NextPlayer;
            }
            userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
            userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
        }

        public bool checkGameEnded()
        {
            int activePlayers = 0;
            string winnerName;
            for(int e = 0; e < numOfPlayers; e++)
            {
                if (!PlayerObjs1[e].GetComponent<Player>().Pdata.HasLost)
                {
                    activePlayers++;
                    winnerName = PlayerObjs1[e].name;
                    userInterface.transform.GetChild(22).GetChild(0).gameObject.GetComponent<TMP_Text>().text = winnerName + " has won the game!";
                }
            }
            if(activePlayers == 1)
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
        
        


        public IEnumerator waitBuyDecision()
        {
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().BuyDecisionPressed == true);
            if (userInterface.GetComponent<InterfaceDisplay>().BuyDecision)
            {
                
                userInterface.GetComponent<InterfaceDisplay>().hideBuyDecisionUI();
                buyS = buyState.ChooseArea;
                PurchaseComplete = false;
                //userInterface.GetComponent<InterfaceDisplay>().BuyDecisionPressed = false;
            }
            else
            {
                userInterface.GetComponent<InterfaceDisplay>().BuyDecisionPressed = false;
                userInterface.GetComponent<InterfaceDisplay>().hideBuyDecisionUI();
                buyS = buyState.EndTransaction;
            }
        }

        public IEnumerator waitForAreaToBuy(GameObject player)
        {
       
            GameObject prevArea = null;
            yield return new WaitUntil(() => AreaToBuy != prevArea);
            if (areaSelected)
            {
                userInterface.GetComponent<InterfaceDisplay>().hideChooseAreaTobuy();
                //Debug.Log("before call completing");
                completePurchase(player);
                exchangeComplete.Play();
                userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
                userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
                buyS = buyState.EndTransaction;
                for (int a = 0; a < 9; a++)
                {
                    GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);                    
                }
            }
        }

        public IEnumerator waitEndTurn()
        {
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().EndTurnPressed == true);
            if (userInterface.GetComponent<InterfaceDisplay>().EndTurnPressed)
            {
                State = turnState.EndTurn;
                userInterface.GetComponent<InterfaceDisplay>().EndTurnPressed = false;
                userInterface.GetComponent<InterfaceDisplay>().hideAfterTurn();
                
            }
        }

        public IEnumerator waitTravelAgain()
        {
            
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().PlayAgainPressed == true);
            if (playAgainChoice)
            {
                userInterface.GetComponent<InterfaceDisplay>().hideCantBuy();
                //userInterface.GetComponent<InterfaceDisplay>().showEndTurnUI();
                State = turnState.RollMoveDie;
                userInterface.GetComponent<InterfaceDisplay>().PlayAgainPressed = false;
                playAgainChoice = false;
            }
            else
            {
                userInterface.GetComponent<InterfaceDisplay>().hideTravelAgainUI();
                canPlayAgain = false;                
            }
        }

        private void completePurchase(GameObject player)
        {
            //Debug.Log("completing1");
            if (AreaToBuy.GetComponent<Area>().OwnerName == "None")
            {
                //Debug.Log("completing2");
                AreaToBuy.GetComponent<Area>().OwnerName = player.name;
                player.GetComponent<Player>().Pdata.Cash -= 1000;
                exchangeComplete.Play();
                PurchaseComplete = true;
                areaSelected = false;
            }
            else
            {
                for (int k = 0; k < NumOfPlayers; k++)
                {
                   if (PlayerObjs1[k].GetComponent<Player>().Pdata.playerName == AreaToBuy.GetComponent<Area>().OwnerName
                        && PlayerObjs1[k].GetComponent<Player>().Pdata.playerName != player.name)
                   {
                        PlayerObjs1[k].GetComponent<Player>().Pdata.Cash += AreaToBuy.GetComponent<Area>().buyingPrice / 2;
                        AreaToBuy.GetComponent<Area>().OwnerName = player.name;
                        player.GetComponent<Player>().Pdata.Cash -= AreaToBuy.GetComponent<Area>().buyingPrice / 2;
                   }                        
                    PurchaseComplete = true;
                    areaSelected = false;

                }
            }

        }

        private int calculateBuildCost()
        {
            buildCost = 0;
            int totalBuilds = AreaToBuild.GetComponent<Area>().Builds + buildCount;
            for (int k = 0; k < totalBuilds; k++)
            {
                if (!AreaToBuild.GetComponent<Area>().buildings[k].activeSelf)
                {
                    buildCost += AreaToBuild.GetComponent<Area>().BuildingPrice[k];
                }
            }
            return buildCost;
        }

        public IEnumerator waitBuildDecision()
        {
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed == true);
            if (userInterface.GetComponent<InterfaceDisplay>().BuildDecision)
            {
                userInterface.GetComponent<InterfaceDisplay>().hideBuildDecisionUI();

                buildS = buildState.ChooseArea;
                BuildComplete = false;
                //userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed = false;
            }
            else
            {
                
                userInterface.GetComponent<InterfaceDisplay>().hideBuildDecisionUI();
                buildS = buildState.EndTransaction;
            }
        }

        public IEnumerator waitForAreaToBuild(GameObject player)
        {
            //Debug.Log("before");
            GameObject prevArea = null;
            yield return new WaitUntil(() => AreaToBuild != prevArea);

            if (areaSelected)
            {
                if (player.GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.BuildNode)
                {
                    //Debug.Log("in BUILD");
                    userInterface.GetComponent<InterfaceDisplay>().hideChooseAreaToBuild();                    
                    areaSelected = false;
                    for (int a = 0; a < 9; a++)
                    {
                        GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                    }
                    userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed = false;
                    buildS = buildState.WaitBuildCount;
                }
                else if(player.GetComponent<Player>().Pdata.CurrentPosTile.GetComponent<Node>().Type == Node.NodeType.FreeBuildNode)
                {
                    //Debug.Log("IN FREE BUILD");
                    areaSelected = false;                    
                    BuildComplete = false;
                    completeFreeBuild();
                    exchangeComplete.Play();
                    for (int a = 0; a < 9; a++)
                    {
                        GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                    }
                    userInterface.GetComponent<InterfaceDisplay>().BuildDecisionPressed = false;
                    userInterface.GetComponent<InterfaceDisplay>().hideChooseAreaToBuild();
                    fBS = freeBuildState.EndTransaction;
                }
            }

        }

        public IEnumerator waitForBuildCount(GameObject player)
        {
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().BuildCountPressed == true);
            if (userInterface.GetComponent<InterfaceDisplay>().BuildCountPressed)
            {
                if (AreaToBuild.GetComponent<Area>().Builds + buildCount <= 3)
                {
                    userInterface.GetComponent<InterfaceDisplay>().hideCantBuildCount();
                    userInterface.GetComponent<InterfaceDisplay>().hideBuildCountDecision();
                    userInterface.GetComponent<InterfaceDisplay>().BuildCountPressed = false;
                    buildS = buildState.RollBuildDie;
                }
                else
                {
                    userInterface.GetComponent<InterfaceDisplay>().showCantBuildCount();
                    userInterface.GetComponent<InterfaceDisplay>().BuildCountPressed = false;
                    buildS = buildState.WaitBuildCount;
                }

            }
        }

        public IEnumerator waitForBuildDie(GameObject player, int bcost)
        {
            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().BuildDieReady == true);
            //Debug.Log("BUILD DIE READY");
            userInterface.GetComponent<InterfaceDisplay>().hideBuildDieUI();
            if (userInterface.GetComponent<InterfaceDisplay>().BuildDieReady)
            {
                //Debug.Log(" indide if");
                userInterface.GetComponent<InterfaceDisplay>().BuildDieReady = false;
                
                if (userInterface.GetComponent<InterfaceDisplay>().BuildDieV == InterfaceDisplay.BuildDieValues.Success)
                {
                    if(checkPlayerMoneyOK(player, bcost))
                    {
                        //Debug.Log("");
                        completeBuild(player, bcost);
                        exchangeComplete.Play();
                        buildS = buildState.EndTransaction;
                    }
                    else
                    {
                        playerLost(player);
                        //Debug.Log("BUILD FAILED SUCCESS");
                        state = turnState.NextPlayer;
                    }
                }
                else if (userInterface.GetComponent<InterfaceDisplay>().BuildDieV == InterfaceDisplay.BuildDieValues.Double)
                {
                    if (checkPlayerMoneyOK(player, 2*bcost))
                    {
                        completeBuild(player, 2*bcost);
                        exchangeComplete.Play();
                        buildS = buildState.EndTransaction;
                    }
                    else
                    {
                        playerLost(player);
                        //Debug.Log("BUILD FAILED DOUBLE");
                        state = turnState.NextPlayer;
                    }
                }
                else if (userInterface.GetComponent<InterfaceDisplay>().BuildDieV == InterfaceDisplay.BuildDieValues.Free)
                {
                    buildS = buildState.EndTransaction;
                    completeBuild(player, 0);
                    exchangeComplete.Play();
                }
                else if (userInterface.GetComponent<InterfaceDisplay>().BuildDieV == InterfaceDisplay.BuildDieValues.Fail)
                {
                    buildS = buildState.EndTransaction;
                }

                State = turnState.NodeAction;
                actionDone = false;
                //actionStarted = false;
                
                

            }

        }

        public void completeBuild(GameObject player, int bcost)
        {
            //Debug.Log("IN BUILD COMPLETE");
            AreaToBuild.GetComponent<Area>().Builds += buildCount;
            for (int k = 0; k < AreaToBuild.GetComponent<Area>().Builds; k++)
            {
                if (!AreaToBuild.GetComponent<Area>().buildings[k].activeSelf)
                {
                    AreaToBuild.GetComponent<Area>().buildings[k].SetActive(true);
                }
            }
            player.GetComponent<Player>().Pdata.Cash -= bcost;
            userInterface.GetComponent<InterfaceDisplay>().updatePlayerStats();
            userInterface.GetComponent<InterfaceDisplay>().updateAreaStats();
            buildComplete = true;
            areaSelected = false;

        }

        public void completeFreeBuild()
        {
            AreaToBuild.GetComponent<Area>().Builds++;
            for (int k = 0; k < areaToBuild.GetComponent<Area>().Builds; k++)
            {
                if (!AreaToBuild.GetComponent<Area>().buildings[k].activeSelf)
                {
                    AreaToBuild.GetComponent<Area>().buildings[k].SetActive(true);
                }
            }
            buildComplete = true;

        }

        public bool entranceAvailable(GameObject player)
        {

            for (int a = 0; a < 9; a++)
            {
                if (GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().OwnerName == player.name
                    && GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().Builds > 0)
                {
                    foreach (GameObject ntile in GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().neighborTiles)
                    {
                        if (!ntile.GetComponent<Node>().HasEntrance)
                        {
                            return true;
                        }
                    }
                    
                }
            }
            return false;
        }

        public void showAvailableEntrances(GameObject player)
        {
            foreach (Transform child in GameObject.Find("BoardNodes").transform)
            {
                child.gameObject.SetActive(false);
            }
            for (int a = 0; a < 9; a++)
            {
                if (GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().OwnerName == player.name
                    && GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().Builds > 0)
                {
                    foreach (GameObject ntile in GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().neighborTiles)
                    {
                        if (!ntile.GetComponent<Node>().HasEntrance)
                        {
                            ntile.SetActive(true);
                        }
                        
                    }

                }

            }
        }

        public IEnumerator waitForTileEntrance(GameObject player)
        {
            if (!TileBothAreas)
            {
                GameObject prevTile = null;
                yield return new WaitUntil(() => EntranceTileToBuild != prevTile);
            }

            if (tileSelected)
            {
                

                if (entranceTileToBuild.GetComponent<Node>().innerAreaObject != null)
                {
                    //Debug.Log("INNERAREA!= NULL");
                    if (entranceTileToBuild.GetComponent<Node>().innerAreaObject.GetComponent<Area>().OwnerName == player.name && waitingForTile)
                    {
                        if (state == turnState.BuyEntrance)
                        {
                            if (checkPlayerMoneyOK(player, 500))
                            {
                                Debug.Log("BOUUUGHT");
                                player.GetComponent<Player>().Pdata.Cash -= 500;
                                entranceTileToBuild.GetComponent<Node>().innerEntranceObject.SetActive(true);
                                entranceTileToBuild.GetComponent<Node>().HasEntrance = true;
                                exchangeComplete.Play();
                                tileSelected = false;
                                waitingForTile = false;
                                BuyEntranceCounter++;
                                
                                if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed)
                                {
                                    foreach (GameObject tile in entranceTileToBuild.GetComponent<Node>().innerAreaObject.GetComponent<Area>().neighborTiles)
                                    {
                                        tile.SetActive(false);
                                    }
                                    //entranceTileToBuild.GetComponent<Node>().innerAreaObject.SetActive(false);
                                }
                                userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed = false;
                            }
                            else
                            {
                                playerLost(player);
                            }
                        }
                        else if (state == turnState.NodeAction)
                        {
                            entranceTileToBuild.GetComponent<Node>().innerEntranceObject.SetActive(true);
                            entranceTileToBuild.GetComponent<Node>().HasEntrance = true;
                            exchangeComplete.Play();

                            tileSelected = false;
                            waitingForTile = false;
                            //Debug.Log("TILE PURCHASED");
                            if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed)
                            {
                                foreach (GameObject tile in entranceTileToBuild.GetComponent<Node>().innerAreaObject.GetComponent<Area>().neighborTiles)
                                {
                                    tile.SetActive(false);
                                }
                                //entranceTileToBuild.GetComponent<Node>().innerAreaObject.SetActive(false);
                            }
                            userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed = false;
                            fES = freeEntranceState.EndTransaction;
                        }

                    }
                }

                if (entranceTileToBuild.GetComponent<Node>().outerAreaObject != null)
                {
                    
                    if (entranceTileToBuild.GetComponent<Node>().outerAreaObject.GetComponent<Area>().OwnerName == player.name && waitingForTile)
                    {
                        if (state == turnState.BuyEntrance)
                        {
                            if (checkPlayerMoneyOK(player, 500))
                            {
                                player.GetComponent<Player>().Pdata.Cash -= 500;
                                entranceTileToBuild.GetComponent<Node>().outerEntranceObject.SetActive(true);
                                entranceTileToBuild.GetComponent<Node>().HasEntrance = true;
                                exchangeComplete.Play();

                                tileSelected = false;
                                waitingForTile = false;
                                BuyEntranceCounter++;
                                Debug.Log("TILE PURCHASED");
                                if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed)
                                {
                                    foreach (GameObject tile in entranceTileToBuild.GetComponent<Node>().outerAreaObject.GetComponent<Area>().neighborTiles)
                                    {
                                        tile.SetActive(false);
                                    }

                                }
                                userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed = false;
                            }
                            else
                            {
                                playerLost(player);
                            }
                        }
                        else if (state == turnState.NodeAction)
                        {
                            entranceTileToBuild.GetComponent<Node>().outerEntranceObject.SetActive(true);
                            entranceTileToBuild.GetComponent<Node>().HasEntrance = true;
                            exchangeComplete.Play();

                            tileSelected = false;
                            waitingForTile = false;
                            Debug.Log("TILE PURCHASED");
                            if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed)
                            {
                                foreach (GameObject tile in entranceTileToBuild.GetComponent<Node>().outerAreaObject.GetComponent<Area>().neighborTiles)
                                {
                                    tile.SetActive(false);
                                }
                                
                            }
                            userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed = false;
                            fES = freeEntranceState.EndTransaction;
                        }
                    }
               }                             
                                
            }


        }

        public void showAllTiles()
        {
            
            for (int a = 0; a < 9; a++)
            {
                foreach(GameObject tile in GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().neighborTiles)
                {
                    tile.SetActive(true);
                }
                //GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                
            }
        }
        public void hideAllTiles()
        {
            Debug.Log("HIDE ALLLLLL");
            for (int a = 0; a < 9; a++)
            {
                foreach (GameObject tile in GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().neighborTiles)
                {
                    tile.SetActive(false);
                }
                //GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
                
            }
        }

        public void showTilesOf(GameObject area)
        {
            
            for (int a = 0; a < 9; a++)
            {
                if (GameObject.Find("Areas").transform.GetChild(a).gameObject.name == area.name)
                {
                    foreach (GameObject tile in GameObject.Find("Areas").transform.GetChild(a).gameObject.GetComponent<Area>().neighborTiles)
                    {
                        if (!tile.GetComponent<Node>().HasEntrance)
                        {
                            tile.SetActive(true);
                        }
                    }
                }
                
                //GameObject.Find("Areas").transform.GetChild(a).gameObject.SetActive(true);
               
            }
        }


        public IEnumerator buyEntrancePoint(GameObject player)
        {
            if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed && !tileSelected)
            {
                hideAllTiles();
            }

            yield return new WaitUntil(() => userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed == true);

            if (userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecision)
            {
                userInterface.GetComponent<InterfaceDisplay>().hideBuyEntranceUI();
                if (BuyEntranceCounter < 9)
                {
                    showTilesOf(GameObject.Find("Areas").transform.GetChild(BuyEntranceCounter).gameObject);
                    waitingForTile = true;
                    StartCoroutine(waitForTileEntrance(player));
                }
                else
                {
                    Debug.Log("CHILD OUT OF BOUNDS BUY ENTRANCE");
                    bES = buyEntranceState.EndTransaction;
                }
                
                
            }
            else
            {
                userInterface.GetComponent<InterfaceDisplay>().hideBuyEntranceUI();
                waitingForTile = false;
                userInterface.GetComponent<InterfaceDisplay>().BuyEntranceDecisionPressed = false;
                BuyEntranceCounter++;
            }
        }



        public void startNewGame()
        {
            Debug.Log("STARTNEWGAME");
            GameObject.Find("NewGame").SetActive(false);
            GameObject.Find("Credits").SetActive(false);
            GameObject.Find("QuitGame").SetActive(false);
            newGameMenu.SetActive(true);
            nopText.SetActive(true);

        }

        public void numOfplayers(int nop)
        {
            newGameMenu.SetActive(false);
            PlayerObjs = new GameObject[nop];
            nopText.SetActive(false);
            StartCoroutine(numOfPlayersCoR(nop));
           

        }

        public IEnumerator numOfPlayersCoR(int nop)
        {
            NumOfPlayers = nop;
            
            for (int i = 0; i < NumOfPlayers; i++)
            {

                textForPlayerNames[i].SetActive(true);

                
                PlayerObjs[i] = (GameObject)Instantiate(playerPrefab);
                PlayerObjs[i].AddComponent<DontDestroy>();


                string prevstr = textForPlayerNames[i].GetComponent<ReadInput>().Input;

                yield return new WaitUntil(() => prevstr != textForPlayerNames[i].GetComponent<ReadInput>().Input);
                PlayerObjs[i].name = textForPlayerNames[i].GetComponent<ReadInput>().input;
                PlayerObjs[i].GetComponent<Player>().Pdata = new PlayerInfo(PlayerObjs[i].name, i, 1000, false, GameObject.Find("Node1"));
                


                textForPlayerNames[i].SetActive(false);
            }
            mainMenuCamera.SetActive(false);
            buttonToclose.SetActive(false);
            figureSelectCamera.SetActive(true);
           
            plFigureSelectText[0].SetActive(true);
            GameObject.Find("StartingCanvas").GetComponent<GameFlow>().plFigureSelectText[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = GameObject.Find("StartingCanvas").GetComponent<GameFlow>().PlayerObjs[0].name + " select your figure!";
        }




        public void showCredits()
        {
            mainMenuCamera.SetActive(false);
            creditsCamera.SetActive(true);
            GameObject.Find("CreditsCanvas").transform.GetChild(1).gameObject.SetActive(true);
            creditsInterface.GetComponent<InterfaceDisplay>().CreditsOn = true;
            creditAnim = GameObject.Find("CreditsCanvas").transform.GetChild(1).gameObject.GetComponent<Animator>();
            creditAnim.GetComponent<Animator>().enabled = true;
            creditAnim.Play("Base Layer.CreditAnimation", 0);
            StartCoroutine(waitForCredit(creditAnim));
            Debug.Log("mhpka");
            
            

        }

        public IEnumerator waitForCredit(Animator creditAnim)
        {
            Debug.Log("perimenw");
            yield return new WaitForSeconds(creditAnim.GetCurrentAnimatorStateInfo(0).length + creditAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            GameObject.Find("CreditsCanvas").transform.GetChild(1).gameObject.SetActive(false);
            creditAnim.GetComponent<Animator>().enabled = false;
            creditsInterface.GetComponent<InterfaceDisplay>().CreditsOn = false;
            creditsCamera.SetActive(false);
            mainMenuCamera.SetActive(true);
        }


        public void quitGame()
        {
            Application.Quit();
        }

    }

}
