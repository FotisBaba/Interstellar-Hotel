using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gameNameSpace
{
    public class FigureSelector : MonoBehaviour
    {

        public GameObject selectedFigure;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseDown()
        {
            Debug.Log("MOUSEDONE");

            for (int i = 0; i < GameObject.Find("StartingCanvas").GetComponent<GameFlow>().NumOfPlayers; i++)
            {

                if (GameObject.Find("StartingCanvas").GetComponent<GameFlow>().plFigureSelectText[i].activeSelf)
                {
                    
                    GameObject.Find("StartingCanvas").GetComponent<GameFlow>().PlayerObjs[i].GetComponent<Player>().Pdata.FigureName = selectedFigure.name;
                    selectedFigure.SetActive(false);

                    GameObject.Find("StartingCanvas").GetComponent<GameFlow>().plFigureSelectText[i].SetActive(false);
                    if (i + 1 <= GameObject.Find("StartingCanvas").GetComponent<GameFlow>().NumOfPlayers - 1)
                    {
                        
                        GameObject.Find("StartingCanvas").GetComponent<GameFlow>().plFigureSelectText[i + 1].SetActive(true);
                        
                        GameObject.Find("StartingCanvas").GetComponent<GameFlow>().plFigureSelectText[i + 1].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = GameObject.Find("StartingCanvas").GetComponent<GameFlow>().PlayerObjs[i + 1].name + " select your figure!";
                        DontDestroyOnLoad(GameObject.Find("StartingCanvas").GetComponent<GameFlow>().PlayerObjs[i]);
                    }
                    else
                    {
                        int nop = GameObject.Find("StartingCanvas").GetComponent<GameFlow>().NumOfPlayers;
                        Scene sceneToLoad = SceneManager.GetSceneByBuildIndex(1);                        
                        SceneManager.LoadScene(1, LoadSceneMode.Single);
                        
                        //while(SceneManager.GetActiveScene().buildIndex != 1) { }
                        
                        
                        //SceneManager.UnloadSceneAsync(0);
                    }
                    break;
                }

            }

        }

       

    }
}
