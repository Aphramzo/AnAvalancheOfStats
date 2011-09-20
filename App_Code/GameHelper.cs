using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GameHelper
/// </summary>
public class GameHelper
{
    private CommonPage Scripts;
	public GameHelper()
	{
        Scripts = new CommonPage();
	}

    public List<Game> GetGameListFromMSSQL()
    {
        var avDBGameList = new List<Game>();
        var connection = Scripts.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "select * from avDBGames";
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            avDBGameList.Add(new Game()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                SeasonId = Convert.ToInt64(Reader.GetValue(1)),
                PreSeason = Scripts.ToSafeBoolean(Reader.GetValue(2)),
                RegularSeason = Scripts.ToSafeBoolean(Reader.GetValue(3)),
                PostSeason = Scripts.ToSafeBoolean(Reader.GetValue(4)),
                Date = Convert.ToDateTime(Reader.GetValue(5)),
                OpponentId = Convert.ToInt64(Reader.GetValue(6)),
                LocationId = Scripts.ToSafeInt(Reader.GetValue(7)),
                OppTypeId = Scripts.ToSafeInt(Reader.GetValue(8)),
                SeriesGame = Scripts.ToSafeInt(Reader.GetValue(9)),
                Win = Scripts.ToSafeBoolean(Reader.GetValue(10)),
                Loss = Scripts.ToSafeBoolean(Reader.GetValue(11)),
                Tie = Scripts.ToSafeBoolean(Reader.GetValue(12)),
                OverTimeWin = Scripts.ToSafeBoolean(Reader.GetValue(13)),
                OverTimeLoss = Scripts.ToSafeBoolean(Reader.GetValue(14)),
                ShootOutWin = Scripts.ToSafeBoolean(Reader.GetValue(15)),
                ShootOutLoss = Scripts.ToSafeBoolean(Reader.GetValue(16)),
                GoalsFor = Scripts.ToSafeInt(Reader.GetValue(17)),
                GoalsAgainst = Scripts.ToSafeInt(Reader.GetValue(18)),
                ShotsFor = Scripts.ToSafeInt(Reader.GetValue(19)),
                ShotsAgainst = Scripts.ToSafeInt(Reader.GetValue(20)),
                PPG = Scripts.ToSafeInt(Reader.GetValue(21)),
                PPOppertunities = Scripts.ToSafeInt(Reader.GetValue(22)),
                PPGA = Scripts.ToSafeInt(Reader.GetValue(23)),
                TimesShortHanded = Scripts.ToSafeInt(Reader.GetValue(24)),
                ShortHandedGoalsFor = Scripts.ToSafeInt(Reader.GetValue(25)),
                ShortHandedGoalsAgainst = Scripts.ToSafeInt(Reader.GetValue(26)),
                Attendence = Scripts.ToSafeInt(Reader.GetValue(27))
            });
        }
        connection.Close();
        return avDBGameList;
    }

    public List<Game> GetGameListFromMySQL()
    {
        var avDBGameList = new List<Game>();
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = "select * from avDBGames";
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            avDBGameList.Add(new Game()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                SeasonId = Convert.ToInt64(Reader.GetValue(1)),
                PreSeason = Scripts.ToSafeBoolean(Reader.GetValue(2)),
                RegularSeason = Scripts.ToSafeBoolean(Reader.GetValue(3)),
                PostSeason = Scripts.ToSafeBoolean(Reader.GetValue(4)),
                Date = Convert.ToDateTime(Reader.GetValue(5)),
                OpponentId = Convert.ToInt64(Reader.GetValue(6)),
                LocationId = Scripts.ToSafeInt(Reader.GetValue(7)),
                OppTypeId = Scripts.ToSafeInt(Reader.GetValue(8)),
                SeriesGame = Scripts.ToSafeInt(Reader.GetValue(9)),
                Win = Scripts.ToSafeBoolean(Reader.GetValue(10)),
                Loss = Scripts.ToSafeBoolean(Reader.GetValue(11)),
                Tie = Scripts.ToSafeBoolean(Reader.GetValue(12)),
                OverTimeWin = Scripts.ToSafeBoolean(Reader.GetValue(13)),
                OverTimeLoss = Scripts.ToSafeBoolean(Reader.GetValue(14)),
                ShootOutWin = Scripts.ToSafeBoolean(Reader.GetValue(15)),
                ShootOutLoss = Scripts.ToSafeBoolean(Reader.GetValue(16)),
                GoalsFor = Scripts.ToSafeInt(Reader.GetValue(17)),
                GoalsAgainst = Scripts.ToSafeInt(Reader.GetValue(18)),
                ShotsFor = Scripts.ToSafeInt(Reader.GetValue(19)),
                ShotsAgainst = Scripts.ToSafeInt(Reader.GetValue(20)),
                PPG = Scripts.ToSafeInt(Reader.GetValue(21)),
                PPOppertunities = Scripts.ToSafeInt(Reader.GetValue(22)),
                PPGA = Scripts.ToSafeInt(Reader.GetValue(23)),
                TimesShortHanded = Scripts.ToSafeInt(Reader.GetValue(24)),
                ShortHandedGoalsFor = Scripts.ToSafeInt(Reader.GetValue(25)),
                ShortHandedGoalsAgainst = Scripts.ToSafeInt(Reader.GetValue(26)),
                Attendence = Scripts.ToSafeInt(Reader.GetValue(27)),
                Source = "MySQL"
            });
        }
        connection.Close();
        return avDBGameList;
    }

    public void UpdateMySQLGame(Game game)
    {
        var sqlToExecute = String.Format("update avDBGames set W = {1}, L = {2}, T = {3}, OTW = {4}, OTL = {5}, SOW = {6}, SOL = {7}, GF = {8}, GA = {9}, PPG = {10}, PPopp = {11}, PPGA = {12}, TS = {13}, SHGF = {14}, SHGA = {15}, Att = {16}, SF = {17}, SA = {18} where id = {0}", 
            game.Id,
            game.Win,
            game.Loss,
            game.Tie,
            game.OverTimeWin,
            game.OverTimeLoss,
            game.ShootOutWin,
            game.ShootOutLoss,
            game.GoalsFor,
            game.GoalsAgainst,
            game.PPG,
            game.PPOppertunities,
            game.PPGA,
            game.TimesShortHanded,
            game.ShortHandedGoalsFor,
            game.ShortHandedGoalsAgainst,
            game.Attendence,
            game.ShotsFor,
            game.ShotsAgainst);
        Scripts.ExecuteMySQLNonQuery(sqlToExecute);
    }

    public void AddGameToMySQL(Game game)
    {
        var sqlToExecute =
            String.Format(
                "insert into avDBGames (Id, SeasonId, Pre, Reg, Pos, Date, OpponentId, LocationId, OppTypeId) values({0},{1},{2},{3},{4},'{5}',{6},{7},{8})",
                game.Id,
                game.SeasonId,
                game.PreSeason,
                game.RegularSeason,
                game.PostSeason,
                Scripts.DateToMySQLDate(game.Date),
                game.OpponentId,
                game.LocationId,
                game.OppTypeId
                );

        Scripts.ExecuteMySQLNonQuery(sqlToExecute);
    }

    public Int64 GetGameFromMySQLByDate(DateTime gameDate)
    {
        var sqlToExecute = String.Format("select id from avDBGames where date = '{0}'", Scripts.DateToMySQLDate(gameDate));
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = sqlToExecute;
        var Reader = command.ExecuteReader();
        Int64 id = 0;
        while (Reader.Read())
        {
            id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("Id")));
        }
        connection.Close();
        return id;
    }
}
