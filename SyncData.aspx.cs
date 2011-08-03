using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

public partial class SyncData : System.Web.UI.Page
{
    protected CommonPage Scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        Scripts = new CommonPage();
        SyncGameData();
        
    }

    private void SyncGameData()
    {
        var helper = new GameHelper();
        var myGames = helper.GetGameListFromMSSQL();
        var hisGames = helper.GetGameListFromMySQL();
        foreach (var game in myGames)
        {
            var hisGame = hisGames.Where(c => c.Date == game.Date).FirstOrDefault();
            if (hisGame == null)
            {
                helper.AddGameToMySQL(game);
                Response.Write(String.Format("Adding game {0}. {1}", game.Date, "<br />"));
            }

            else if (game.ShotsFor != hisGame.ShotsFor)
            {
                hisGame.Win = game.Win;
                hisGame.Loss = game.Loss;
                hisGame.Tie = game.Tie;
                hisGame.OverTimeLoss = game.OverTimeLoss;
                hisGame.OverTimeWin = game.OverTimeWin;
                hisGame.ShootOutLoss = game.ShootOutLoss;
                hisGame.ShootOutWin = game.ShootOutWin;
                hisGame.GoalsAgainst = game.GoalsAgainst;
                hisGame.GoalsFor = game.GoalsFor;
                hisGame.ShotsFor = game.ShotsFor;
                hisGame.ShotsAgainst = game.ShotsAgainst;
                hisGame.PPG = game.PPG;
                hisGame.PPOppertunities = game.PPOppertunities;
                hisGame.PPGA = game.PPGA;
                hisGame.TimesShortHanded = game.TimesShortHanded;
                hisGame.ShortHandedGoalsAgainst = game.ShortHandedGoalsAgainst;
                hisGame.ShortHandedGoalsFor = game.ShortHandedGoalsFor;
                hisGame.Attendence = game.Attendence;
                helper.UpdateMySQLGame(hisGame);
                Response.Write(String.Format("Updating game {0}. {1}", game.Date, "<br />"));
            }
        }


        
    }

    private void SyncPlayerData()
    {
        var helper = new PlayerHelper();
        var hisPlayers = helper.GetPlayerListFromMySQL();
        var myPlayers = helper.GetPlayerListFromMSSQL();
        foreach(var player in myPlayers)
        {
            var hisPlayer = hisPlayers.Where(c => c.Id == player.Id).FirstOrDefault();
            if(hisPlayer == null)
            {
                helper.AddPlayerToMySQL(player);
                Response.Write(String.Format("Adding player {0} {1}", player.Name, "<br />"));
            }
            else if(hisPlayer.IsCurrent != player.IsCurrent)
            {
                hisPlayer.IsCurrent = player.IsCurrent;
                helper.UpdateMySQLPlayer(hisPlayer);

                Response.Write(String.Format("Updating {0}'s IsCurrent to {1} {2}", player.Name, player.IsCurrent.ToString(), "<br />"));
            }

        }

    }
}
