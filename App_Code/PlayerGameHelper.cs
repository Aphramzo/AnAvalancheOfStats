﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlayerGameHelper
/// </summary>
public class PlayerGameHelper
{
    private CommonPage Scripts;
    public PlayerGameHelper()
    {
        Scripts = new CommonPage();
    }

    public List<PlayerGame> GetPlayerListFromMSSQLBySeasonName(String seasonName)
    {
        var playerGameList = new List<PlayerGame>();
        var connection = Scripts.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select avDBSkaterRS.* From avDBSkaterRS inner join avDBGames on avDBSkaterRS.GameId = avDBGames.Id inner join avDBSeason on avDBGames.SeasonId = avDBSeason.Id where avDBSeason.Year = '{0}'",seasonName);
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            playerGameList.Add(new PlayerGame()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                Game = new Game(){Id=Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("GameId")))},
                Player = new App_Code.Player(){Id=Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("PlayerId")))},
                Stats = new Statistics(){
                    Goals = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("G"))),
                    Assists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("A"))),
                    AttemptsBlocked = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("AttemptsBlocked"))),
                    BlockedShots = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("BlockedShots"))),
                    EmptyNet = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("EN"))),
                    EmptyNetAssits = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("ENAssists"))),
                    ENTimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("EVTOI"))),
                    FaceOffsLost = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("FaceOffsLost"))),
                    FaceOffsWon = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("FaceOffsWon"))),
                    GiveAways = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("GiveAways"))),
                    GW = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("GW"))),
                    GWAssists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("GWAssists"))),
                    Hits = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("Hits"))),
                    PenaltiesTaken = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PenaltiesTaken"))),
                    PIM = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PIM"))),
                    PlusMinus = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("+/-"))),
                    PP = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PP"))),
                    PPAssists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PPAssists"))),
                    PPTimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("PPTOI"))),
                    SH = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SH"))),
                    SHAssists = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SHAssists"))),
                    SHTimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("SHTOI"))),
                    Shots = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("S"))),
                    ShotsMissed = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("ShotsMissed"))),
                    TakeAways = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("TakeAways"))),
                    TimeOnIce = Convert.ToInt32(Reader.GetValue(Reader.GetOrdinal("TOI")))
                }
            });
        }
        connection.Close();
        return playerGameList;
    }

    public List<PlayerGame> GetPlayerListFromMySQLBySeasonName(String seasonName)
    {
        var playerGameList = new List<PlayerGame>();
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select avDBSkaterRS.* From avDBSkaterRS inner join avDBGames on avDBSkaterRS.GameId = avDBGames.Id inner join avDBSeason on avDBGames.SeasonId = avDBSeason.Id where avDBSeason.Year = '{0}'", seasonName);
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            playerGameList.Add(new PlayerGame()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                Game = new Game() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("GameId"))) },
                Player = new App_Code.Player() { Id = Convert.ToInt64(Reader.GetValue(Reader.GetOrdinal("PlayerId"))) },
                Stats = new Statistics()
                {
                    Goals = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("G"))),
                    Assists = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("A"))),
                    AttemptsBlocked = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("AttemptsBlocked"))),
                    BlockedShots = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("BlockedShots"))),
                    EmptyNet = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("EN"))),
                    EmptyNetAssits = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("ENAssists"))),
                    ENTimeOnIce = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("EVTOI"))),
                    FaceOffsLost = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("FaceOffsLost"))),
                    FaceOffsWon = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("FaceOffsWon"))),
                    GiveAways = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("GiveAways"))),
                    GW = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("GW"))),
                    GWAssists = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("GWAssists"))),
                    Hits = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("Hits"))),
                    PenaltiesTaken = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PenaltiesTaken"))),
                    PIM = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PIM"))),
                    PlusMinus = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PlusMinus"))),
                    PP = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PP"))),
                    PPAssists = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PPAssists"))),
                    PPTimeOnIce = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("PPTOI"))),
                    SH = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("SH"))),
                    SHAssists = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("SHAssists"))),
                    SHTimeOnIce = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("SHTOI"))),
                    Shots = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("S"))),
                    ShotsMissed = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("ShotsMissed"))),
                    TakeAways = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("TakeAways"))),
                    TimeOnIce = Scripts.ToSafeInt(Reader.GetValue(Reader.GetOrdinal("TOI")))
                }
            });
        }
        connection.Close();
        return playerGameList;
    }
}
