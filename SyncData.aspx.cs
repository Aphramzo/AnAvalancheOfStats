﻿using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

public partial class SyncData : System.Web.UI.Page
{
    protected CommonPage Scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            return;
        Scripts = new CommonPage();
        if(!Scripts.GoodPassWord(Request["pwd"]))
        {
            WrongPassword.Visible = true;
            return;
        }
        InfoLabel.Text = "<div style=\"padding: 0 .7em;\" class=\"ui-state-highlight ui-corner-all\"><p><span style=\"float: left; margin-right: .3em;\" class=\"ui-icon ui-icon-warning\"></span>";
        SyncPlayerData();
        SyncGameData();
        SyncPlayerGameData();
        RegenTables();
        InfoLabel.Text += "</div>";
        InfoLabel.Visible = true;
    }

    #region Regeners
    private void RegenTables(){
        var sql = "truncate table avDBPlayerSeason";
        Scripts.ExecuteMySQLNonQuery(sql);
        sql = "insert into avDBPlayerSeason select * from avDBvPlayerSeason";
        Scripts.ExecuteMySQLNonQuery(sql);

    }
    #endregion

    #region PlayerGames

    private void SyncPlayerGameData()
    {
        var helper = new PlayerGameHelper();
        var myPlayerGames = helper.GetPlayerListFromMSSQLBySeasonName("2011-2012");
        var hisPlayerGames = helper.GetPlayerListFromMySQLBySeasonName("2011-2012");
        foreach (var playerGame in myPlayerGames)
        {
            var hisPlayerGame = hisPlayerGames.Where(c => c.Game.Date == playerGame.Game.Date && c.Player.Id == playerGame.Player.Id).FirstOrDefault();
            if (hisPlayerGame == null)
            {
                AddPlayerGame(helper, playerGame);
            }
        }
    }

    private void AddPlayerGame(PlayerGameHelper helper, PlayerGame playerGame)
    {
        helper.AddPlayerGameToMySQL(playerGame);
        InfoLabel.Text += String.Format("Adding player game for {0} on {1}. {2}", playerGame.Player.Id, playerGame.Game.Date, "<br />");
    }
    #endregion

    #region Games
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
                AddGame(helper, game);
            }

            else if (game.ShotsFor != hisGame.ShotsFor)
            {
                UpdateGame(helper, game, hisGame);
            }
        }
    }

    private void UpdateGame(GameHelper helper, Game game, Game hisGame)
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
        InfoLabel.Text += String.Format("Updating game {0}. {1}", game.Date, "<br />");
    }

    private void AddGame(GameHelper helper, Game game)
    {
        helper.AddGameToMySQL(game);
        InfoLabel.Text += String.Format("Adding game {0}. {1}", game.Date, "<br />");
    }

    #endregion

    #region Players

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
                AddPlayer(helper, player);
            }
            else if(hisPlayer.IsCurrent != player.IsCurrent)
            {
                UpdatePlayer(helper, player, hisPlayer);
            }

        }
    }

    private void UpdatePlayer(PlayerHelper helper, App_Code.Player player, App_Code.Player hisPlayer)
    {
        hisPlayer.IsCurrent = player.IsCurrent;
        helper.UpdateMySQLPlayer(hisPlayer);

        InfoLabel.Text += String.Format("Updating {0}'s IsCurrent to {1} {2}", player.Name, player.IsCurrent.ToString(), "<br />");
    }

    private void AddPlayer(PlayerHelper helper, App_Code.Player player)
    {
        helper.AddPlayerToMySQL(player);
        InfoLabel.Text += String.Format("Adding player {0} {1}", player.Name, "<br />");
    }
    #endregion

    #region GoalieGames

    private void SyncGoalieGameData()
    {
        var helper = new GoalieGameHelper();
        var myPlayerGames = helper.GetGoalieListFromMSSQLBySeasonName("2011-2012");
        var hisPlayerGames = helper.GetGoalieListFromMySQLBySeasonName("2011-2012");
        foreach (var playerGame in myPlayerGames)
        {
            var hisPlayerGame = hisPlayerGames.Where(c => c.Game.Date == playerGame.Game.Date && c.Player.Id == playerGame.Player.Id).FirstOrDefault();
            if (hisPlayerGame == null)
            {
                AddGoalieGame(helper, playerGame);
            }
        }
    }

    private void AddGoalieGame(GoalieGameHelper helper, GoalieGame playerGame)
    {
        helper.AddGoalieGameToMySQL(playerGame);
        InfoLabel.Text += String.Format("Adding goalie game for {0} on {1}. {2}", playerGame.Player.Id, playerGame.Game.Date, "<br />");
    }
    #endregion
}
