using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoalieGameHelper
/// </summary>
public class GoalieGameHelper
{
    private CommonPage Scripts;
	public GoalieGameHelper()
	{
		  Scripts = new CommonPage();
    }

    public List<GoalieGame> GetGoalieListFromMSSQLBySeasonName(String seasonName)
    {
        var playerGameList = new List<GoalieGame>();
        var connection = Scripts.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select avDBGoalieRS.* From avDBGoalieRS inner join avDBGames on avDBGoalieRS.GameId = avDBGames.Id inner join avDBSeason on avDBGames.SeasonId = avDBSeason.Id where avDBSeason.Year = '{0}'", seasonName);
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            playerGameList.Add(new GoalieGame()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                Game = new Game() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("GameId"))), Source = "MSSQL" },
                Player = new App_Code.Player() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("PlayerId"))) },
                Goals = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("G"))),
                ShotAgaisnt = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SA"))),
                Assists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("A"))),
                GoalsAgainst = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("GA"))),
                Saves = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SV"))),
                SOG = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SOG"))),
                Shutout = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("SO"))),
                Win = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("W"))),
                Loss = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("L"))),
                Tie = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("T"))),
                OT = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("OT"))),
                ShootOut = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("SOUT"))),
                PIM = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PIM"))),
                TimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("TOI")))
                
            });
        }
        connection.Close();
        return playerGameList;
    }

    public List<GoalieGame> GetGoalieListFromMySQLBySeasonName(String seasonName)
    {
        var playerGameList = new List<GoalieGame>();
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select avDBGoalieRS.* From avDBGoalieRS inner join avDBGames on avDBGoalieRS.GameId = avDBGames.Id inner join avDBSeason on avDBGames.SeasonId = avDBSeason.Id where avDBSeason.Year = '{0}'", seasonName);
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            playerGameList.Add(new GoalieGame()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                Game = new Game() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("GameId"))), Source = "MySQL" },
                Player = new App_Code.Player() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("PlayerId"))) },
                Goals = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("G"))),
                ShotAgaisnt = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SA"))),
                Assists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("A"))),
                GoalsAgainst = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("GA"))),
                Saves = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SV"))),
                SOG = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SOG"))),
                Shutout = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("SO"))),
                Win = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("W"))),
                Loss = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("L"))),
                Tie = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("T"))),
                OT = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("OT"))),
                ShootOut = Scripts.ToSafeBoolean(Reader.GetValue(Reader.GetOrdinal("SOUT"))),
                PIM = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PIM"))),
                TimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("TOI")))
                
            });
        }
        connection.Close();
        return playerGameList;
    }

    public void AddGoalieGameToMySQL(GoalieGame playerGame)
    {
        var helper = new GameHelper();
        var hisGame = helper.GetGameFromMySQLByDate(playerGame.Game.Date);
        var sqlToExecute = String.Format(
            "insert into avDBGoalieRS (PlayerId, GameId, G, A, PIM, W, L, OT, T, SOUT, GA, SA, SV, SO, SOG, TOI)" +
            "values ({0},{1}, {2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})",
                playerGame.Player.Id,
                hisGame,
                playerGame.Goals,
                playerGame.Assists,
                playerGame.PIM,
                playerGame.Win,
                playerGame.Loss,
                playerGame.OT,
                playerGame.Tie,
                playerGame.ShootOut,
                playerGame.GoalsAgainst,
                playerGame.ShotAgaisnt,
                playerGame.Saves,
                playerGame.Shutout,
                playerGame.SOG,
                playerGame.TimeOnIce
            );
        Scripts.ExecuteMySQLNonQuery(sqlToExecute);
    }
}
