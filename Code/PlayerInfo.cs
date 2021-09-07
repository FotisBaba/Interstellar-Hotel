using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gameNameSpace
{
    [System.Serializable]
    public class PlayerInfo
    {
        private string figureName;
        private GameObject figure;

        private GameObject currentPosTile;
        private GameObject previousPosTile;

        [SerializeField] public string playerName;
        [SerializeField] private int playerID;
        [SerializeField] private int cash;
        [SerializeField] private bool hasLost;
        internal object playername;

        public GameObject Figure { get => figure; set => figure = value; }
        public global::System.String PlayerName { get => playerName; set => playerName = value; }
        public global::System.Int32 PlayerID { get => playerID; set => playerID = value; }
        public global::System.Int32 Cash { get => cash; set => cash = value; }
        public global::System.Boolean HasLost { get => hasLost; set => hasLost = value; }
        public GameObject CurrentPosTile { get => currentPosTile; set => currentPosTile = value; }
        public GameObject PreviousPosTile { get => previousPosTile; set => previousPosTile = value; }
        public string FigureName { get => figureName; set => figureName = value; }

        public PlayerInfo(string name, int ID, int money, bool lost, GameObject currTile)
        {

            this.Figure = null;

            this.CurrentPosTile = currTile;
            this.PreviousPosTile = null;

            this.PlayerName = name;
            this.PlayerID = ID;
            this.Cash = money;
            this.HasLost = lost;
        }
    }
}
