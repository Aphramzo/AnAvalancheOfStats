﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class Player : System.Web.UI.Page
{
    private CommonPage scripts;
    private String playerName;
    private int playerId;
    private String dob;
    private String country;
    private String position;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        playerId = Convert.ToInt32(Request["id"]);
        loadPlayerInfo();
        AppendDataToGrid();
    }

    protected void loadPlayerInfo()
    {
        String sqlString = String.Format("select name, dob, country, position from vPlayer where id = {0}", playerId);
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                playerName = Convert.ToString(reader[0]);
                dob = Convert.ToString(reader[1]);
                country = Convert.ToString(reader[2]);
                position = Convert.ToString(reader[3]);
            }
        }
    }

    private void AppendDataToGrid()
    {
        String sqlString = String.Format("select {0} from vPlayerSeason {1}", GenerateSelectColumns(), " where playerId = " + playerId);
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgSeasons.DataBind();

        String sqlString2 = String.Format("spPlayerSeasonTotalsWithRanks {0}", playerId);
        SqlCommand gridCommand2 = new SqlCommand(sqlString2, scripts.GetConnection());

        dgSeasonTotals.DataSource = gridCommand2.ExecuteReader(CommandBehavior.CloseConnection);
        dgSeasonTotals.DataBind();

        String sqlString1 = String.Format("select {0} from vPlayerPlayoff {1}", GenerateSelectColumns(), " where playerId = " + playerId);
        SqlCommand gridCommand1 = new SqlCommand(sqlString1, scripts.GetConnection());

        dgPlayoffs.DataSource = gridCommand1.ExecuteReader(CommandBehavior.CloseConnection);
        dgPlayoffs.DataBind();

        String sqlString3 = String.Format("select {0} from avDBvPlayerSplitsMonth {1}", GenerateSplitMonthsSelectColumns(), " where playerId = " + playerId);
        SqlCommand gridCommand3 = new SqlCommand(sqlString3, scripts.GetConnection());

        dgSplitsMonth.DataSource = gridCommand3.ExecuteReader(CommandBehavior.CloseConnection);
        dgSplitsMonth.DataBind();

        String sqlString4 = String.Format("select {0} from avDBvPlayerSplitsOpponent {1}", GenerateSplitOpponentSelectColumns(), " where playerId = " + playerId);
        SqlCommand gridCommand4 = new SqlCommand(sqlString4, scripts.GetConnection());

        dgSplitsOpponent.DataSource = gridCommand4.ExecuteReader(CommandBehavior.CloseConnection);
        dgSplitsOpponent.DataBind();

        String sqlString5 = String.Format("select {0} from avDBvPlayerSplitsHomeAway {1}", GenerateSplitHomeAwaySelectColumns(), " where playerId = " + playerId);
        SqlCommand gridCommand5 = new SqlCommand(sqlString5, scripts.GetConnection());

        dgSplitsHomeAway.DataSource = gridCommand5.ExecuteReader(CommandBehavior.CloseConnection);
        dgSplitsHomeAway.DataBind();

    }

    private string GenerateSelectColumns()
    {
        return " Description as Season, " +
                "Age, " +
                "GP, " +
                "Goals," +
                "Assists, " +
                "Points," +
                "PIM, " +
                "PlusMinus as [+/-], " +
                "PP, " +
                "SH, " +
                "GW, " +
                "Shots, " +
                "ATOI, " +
                "ShiftsPerGame as [Shifts Per Game], " +
                "GoalsPerGame as [Goals Per Game], " +
                "PointsPerGame as [Points Per Game], " +
                "PIMPerGame as [PIM Per Game]";
    }

    private string GenerateSplitMonthsSelectColumns()
    {
        return " SeasonName as Season, " +
                "Month, " +
                SplitsCommonColumns();
    }

    private string GenerateSplitOpponentSelectColumns()
    {
        return "Team, " +
            SplitsCommonColumns();
    }

    private string GenerateSplitHomeAwaySelectColumns()
    {
        return "Location, " +
            SplitsCommonColumns();
    }

    private string SplitsCommonColumns()
    {
        return "GP, " +
                "Goals," +
                "Assists, " +
                "Points," +
                "PIM, " +
                "PlusMinus as [+/-], " +
                "PP, " +
                "SH, " +
                "GW, " +
                "Shots, " +
                "ATOI, " +
                "GoalsPerGame as [Goals Per Game], " +
                "PointsPerGame as [Points Per Game], " +
                "PIMPerGame as [PIM Per Game]";
    }

    public string PlayerName()
    {
        return playerName;
    }

    public string DOB()
    {
        return dob;
    }

    public string Country()
    {
        return country;
    }

    public string Position()
    {
        return position;
    }
}
