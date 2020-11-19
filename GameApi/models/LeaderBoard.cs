namespace GameApi.models
{
    public class LeaderBoard
    {
        public string userName { get; set; }
        public double winRatio { get; set; }
        public int gamesPlayed { get; set; }
        public string Last5Games { get; set; }

        public void setUserName(string username){
            this.userName = username;
        }

        public void CalcWinRatio(int gamesWon, int gamesplayed){
            //GamesWon and GamesPlayed need to be set first for this to work
            this.winRatio = gamesWon / gamesplayed ;
        }

        public void setGamesPlayed(int gamesplayed){
            this.gamesPlayed = gamesplayed;
        }

        public void setLast5Games(string[] demgames){
            //needs to be pulled from SQL
            //also think this should be a string array
            int i = 0;
            foreach (var game in demgames)
            {
              this.Last5Games = demgames[i];  
              i++;
            }
           
        }
    }
}