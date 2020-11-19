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
    Gameresults Game2 = new Gameresults();

    public GameController(IConfiguration iconfig){
            this.configuration = iconfig;
            this.stringBuilder.DataSource = this.configuration.GetSection("DbConnectionStrings").GetSection("Url").Value;
            this.stringBuilder.InitialCatalog = this.configuration.GetSection("DbConnectionStrings").GetSection("Database").Value;
            this.stringBuilder.UserID = this.configuration.GetSection("DbConnectionStrings").GetSection("User").Value;
            this.stringBuilder.Password = this.configuration.GetSection("DbConnectionStrings").GetSection("Password").Value;
            this.connectionString = this.stringBuilder.ConnectionString;
        }
    
     [HttpPost]
      public Gameresults PlayRequest(Player Poop){
          //set player results in game
          this.Game2.SetPlayerChoice(Poop.PlayerChoice, Poop.UserName);
          //set CPU choice
          this.Game2.CpuRandChoice();
          //commit results
          this.Game2.CommitResult(Game2.PlayerChoice, Game2.CpuChoice);
          //this.Game2.CommitResult(Game2.PlayerChoice,Game2.CpuChoice);

          return Game2;
      } 

      [HttpPost("rounds")]
      public List<Gameresults> PlayRequests(Player[] Poop){
          DateTime datie = DateTime.Now;
          //set player results into list (probs a foreach)
          int i = 0;
          foreach (var Player in Poop)
          {
            this.Game2 = new Gameresults();
            this.Game2.SetPlayerChoice(Poop[i].PlayerChoice, Poop[i].UserName);
            this.Game2.CpuRandChoice();
            this.Game2.CommitResult(Game2.PlayerChoice, Game2.CpuChoice);
            i++;
              //dunno how to do it here
            resulty.Add(Game2);
          }
          this.Game2.SetFinalResult();
          //sql commands save time method, save playername method, save game method, save rounds method (with all send in datie)
          return resulty;
      }
      //  [HttpGet("leaderBoard")]
      //  public List<list> 
      //  list add 5 lines of info
      
    }
}
