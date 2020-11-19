using System;

namespace GameApi.models
{
    public class Player
    {
        public string PlayerChoice { get; set; }
        public string UserName { get; set; }
        public int MaxRounds {get; set;}
        public int CurrentRound {get; set;}

        public void SetPlayerChoice(string pChooseth, string uName, int cRound, int mRounds){
            this.PlayerChoice = pChooseth;
            this.UserName = uName;
            this.CurrentRound = cRound;
            this.MaxRounds = mRounds;
        }
    }
}
