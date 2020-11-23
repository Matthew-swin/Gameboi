using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameApi.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;


namespace GameApi.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
    public List<Gameresults> resulty = new List<Gameresults>();
    SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
    IConfiguration configuration;
    string connectionString;
    public Gameresults Game2 = new Gameresults();

    public GameController(IConfiguration iconfig){
            this.configuration = iconfig;
            this.stringBuilder.DataSource = this.configuration.GetSection("DbConnectionStrings").GetSection("Url").Value;
            this.stringBuilder.InitialCatalog = this.configuration.GetSection("DbConnectionStrings").GetSection("Database").Value;
            this.stringBuilder.UserID = this.configuration.GetSection("DbConnectionStrings").GetSection("User").Value;
            this.stringBuilder.Password = this.configuration.GetSection("DbConnectionStrings").GetSection("Password").Value;
            this.connectionString = this.stringBuilder.ConnectionString;
        }
    

      [HttpPost("rounds")]
      public List<Gameresults> PlayRequests(Player[] Poop){
          DateTime datie = DateTime.Now;
          //set player results into list (probs a foreach)
          int i = 0;
          foreach (var Player in Poop)
          {
            this.Game2 = new Gameresults();
            this.Game2.SetPlayerChoice(Poop[i].PlayerChoice, Poop[i].UserName, Poop[i].CurrentRound, Poop[i].MaxRounds);
            this.Game2.CpuRandChoice();
            this.Game2.CommitResult(Game2.PlayerChoice, Game2.CpuChoice);
            i++;
              //dunno how to do it here
            resulty.Add(Game2);
          }
          //save final result
          this.Game2.SetFinalResult();
          //sql commands save time method, save Game, save Turn (with all send in datie)
          this.Game2.AddGameToDb(Poop[i].MaxRounds, this.connectionString,datie);
          return resulty;
      }
      [HttpGet("leaderBoard")]
      public List<LeaderBoardResponse> getLeaderboardData(string connectionString)
        {

            List<LeaderBoardResponse> customers = new List<LeaderBoardResponse>();

            SqlConnection conn = new SqlConnection(connectionString);

            string queryString = "Select * From LEADERBOARD";

            SqlCommand command = new SqlCommand(queryString, conn);
            conn.Open();

            string result = "";
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result += reader[0] + " | " + reader[1] + reader[2] + reader[3] + reader[4] + "\n";

                    customers.Add(
                        new LeaderBoardResponse()
                        {
                            username = reader[0].ToString(),
                            winRatio = Math.Round(((double)reader[1]), 2),
                            gamesPlayed = (int)reader[2],
                            roundsPlayed = (int)reader[3],
                            last5Games = reader[4].ToString()
                        });
                }
            }

            return customers;
        }
      
    }
}
