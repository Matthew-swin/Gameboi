using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace GameApi.models
{
    public class Gameresults
    {
        public string PlayerChoice { get; set; }
        public string CpuChoice { get; set; }
        public string TurnResult { get; set; }
        public string GameResult { get; set; }
        public string UserName {get; set;}
        public int MaxRounds {get; set;}
        public int CurrentRound {get; set;}
        public int PlayerScore = 0;
        public int CpuScore = 0;
        

        public void setMaxRound(int Round){
            this.MaxRounds = Round;

        }
        public void setCurrentRound(int Round){
            this.CurrentRound = Round;

        }
        //Set the playerChoice
        public void SetPlayerChoice(string pChooseth, string uName, int cRound, int mRounds)
        {
            this.PlayerChoice = pChooseth;
            this.UserName = uName;
            this.CurrentRound = cRound;
            this.MaxRounds = mRounds;
            

        }
        public void CpuRandChoice()
        {
            // selecting random choice by setting array with possible values
            string[] ChoiceArray = { "Rock", "Paper", "Scissors" };
            //setting up rand
            Random random = new Random();
            //create rand int dependant on array length
            int index = random.Next(ChoiceArray.Length);
            //return variable from array with rand index
            this.CpuChoice = ChoiceArray[index];
        }

        public void CommitResult(string PlayerChoice, string CpuChoice)
        {
            //Set the result to win if meets the below conditions
            if (PlayerChoice == "Rock" && CpuChoice == "Scissors" || PlayerChoice == "Scissors" && CpuChoice == "Paper" || PlayerChoice == "Paper" && CpuChoice == "Rock")
            {
                this.TurnResult = "Win";
                this.PlayerScore++;
            }
            //Set the result to win if meets the below conditions
            else if (PlayerChoice == "Rock" && CpuChoice == "Rock" || PlayerChoice == "Scissors" && CpuChoice == "Scissors" || PlayerChoice == "Paper" && CpuChoice == "Paper")
            {
                this.TurnResult = "Draw";
            }
            //Set the result to lose as all other ways have been covered
            else
            {
                this.TurnResult = "Lose";
                this.CpuScore++;
            }


        }
        public void SetFinalResult(){
            if(this.PlayerScore > this.CpuScore){
                this.GameResult = "Win";
            }
            else if(this.CpuScore > this.PlayerScore){
                this.GameResult = "Lose";
            }
            else{
                this.GameResult = "Draw";
            }
        }

        public string AddGameToDb(int gamemaxrounds, string connectionString, DateTime cdt)
        {

            SqlConnection conn = new SqlConnection(connectionString);

            
            string queryString = "INSERT INTO GAME VALUES ('" + this.PlayerChoice + "','" + cdt.ToString("MM/dd/yyyy hh:mm:ss") + "'," + gamemaxrounds + ",'" + this.GameResult[0] + "')";

            SqlCommand command = new SqlCommand(queryString, conn);
            conn.Open();

            try
            {
                var result = command.ExecuteNonQuery();
                return result.ToString();
            }
            catch (SqlException se)
            {
                return "user already exists " + se.Message;
            }

        }

    }
}