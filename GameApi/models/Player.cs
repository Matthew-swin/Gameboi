using System;

namespace GameApi.models
{
    public class Player
    {
        public string PlayerChoice { get; set; }
        public string UserName { get; set; }

        public void SetPlayerChoice(string playerChooseth, string userName){
            this.PlayerChoice = playerChooseth;
            this.UserName = userName;
        }
    }
}
