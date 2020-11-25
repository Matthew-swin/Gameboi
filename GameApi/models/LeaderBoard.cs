using System;



namespace GameApi.models
{
    public class LeaderBoardResponse
    {
        public string username { get; set; }
        public double winRatio { get; set; }
        public int gamesPlayed { get; set; }
        public string last5Games { get; set; }
  

    }
}