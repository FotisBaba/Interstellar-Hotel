using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace
{
    public class PlayerData
    {

        private GameObject figure;

        private GameObject currentPos;
        private GameObject previousPos;

        [SerializeField] public string playerName;
        [SerializeField] private int playerID;
        [SerializeField] private int cash;
        [SerializeField] private bool hasLost;

        public GameObject Figure { get => figure; set => figure = value; }
        public GameObject CurrentPos { get => currentPos; set => currentPos = value; }
        public GameObject PreviousPos { get => previousPos; set => previousPos = value; }
        public global::System.String PlayerName { get => playerName; set => playerName = value; }
        public global::System.Int32 PlayerID { get => playerID; set => playerID = value; }
        public global::System.Int32 Cash { get => cash; set => cash = value; }
        public global::System.Boolean HasLost { get => hasLost; set => hasLost = value; }



        public PlayerData(string name, int ID, int money, bool lost)
        {

            this.Figure = null;

            this.CurrentPos = null;
            this.PreviousPos = null;

            this.PlayerName = name;
            this.PlayerID = ID;
            this.Cash = money;
            this.HasLost = lost;
        }

    }

}
