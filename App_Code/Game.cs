using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Game
/// </summary>
public class Game
{
    public Int64 Id { get; set; }
    public Int64 SeasonId { get; set; }
    public bool PreSeason { get; set; }
    public bool RegularSeason { get; set; }
    public bool PostSeason { get; set; }
    private DateTime date { get; set; }
    public DateTime Date 
    {
        get
        {
            if (date != DateTime.MinValue)
                return date;

            LoadFromSource();
            return date;
        }
        set
        {
            date = value;
        }
    }
    public Int64 OpponentId { get; set; }
    public int LocationId { get; set; }
    public int OppTypeId { get; set; }
    public int SeriesGame { get; set; }
    public bool Win { get; set; }
    public bool Loss { get; set; }
    public bool Tie { get; set; }
    public bool OverTimeWin { get; set; }
    public bool OverTimeLoss { get; set; }
    public bool ShootOutWin { get; set; }
    public bool ShootOutLoss { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int ShotsFor { get; set; }
    public int ShotsAgainst { get; set; }
    public int PPG { get; set; }
    public int PPOppertunities { get; set; }
    public int PPGA { get; set; }
    public int TimesShortHanded { get; set; }
    public int ShortHandedGoalsFor { get; set; }
    public int ShortHandedGoalsAgainst { get; set; }
    public int Attendence { get; set; }

    public String Source { get; set; }

	public Game()
	{
		
	}

    private void LoadFromSource()
    {
        if (Source == "MySQL")
            LoadFromMySQL();

        if (Source == "MSSQL")
            LoadFromMSSQL();
    }

    private void LoadFromMySQL()
    {
        var Scripts = new CommonPage();
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select * from avDBGames where id = {0}", Id);
         var Reader = command.ExecuteReader();
         while (Reader.Read())
         {
             Date = Convert.ToDateTime(Reader.GetValue(Reader.GetOrdinal("Date")));
         }
         connection.Close();
    }

    private void LoadFromMSSQL()
    {
        var Scripts = new CommonPage();
        var connection = Scripts.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = String.Format("select * from avDBGames where id = {0}", Id);
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            Date = Convert.ToDateTime(Reader.GetValue(Reader.GetOrdinal("Date")));
        }
        connection.Close();
    }
}
