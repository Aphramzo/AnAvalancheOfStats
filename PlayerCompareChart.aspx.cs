﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PlayerCompareChart : System.Web.UI.Page
{
    private CommonPage scripts;
    private double player1Id;
    private double player2Id;
    private string player1Name;
    private string player2Name;
    private string statId;
    private int modBy;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        player1Id = Convert.ToDouble(Request["Player1Id"]);
        player2Id = Convert.ToDouble(Request["Player2Id"]);
        statId = Convert.ToString(Request["stat"]);
        SetNames();
    }

    private void SetNames()
    {
        String sqlString = String.Format("select Name from avDBPlayer where id = {0}", player1Id);
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                player1Name = Convert.ToString(reader[0]);
            }
        }

        String sqlString1 = String.Format("select Name from avDBPlayer where id = {0}", player2Id);
        SqlCommand sqlCommand1 = new SqlCommand(sqlString1, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand1.ExecuteReader())
        {
            while (reader.Read())
            {
                player2Name = Convert.ToString(reader[0]);
            }
        }

        String sqlString2 = String.Format("select count(id), playerId from avDBSkaterRS where playerId in ({0},{1}) group by PlayerId", player2Id, player1Id);
        SqlCommand sqlCommand2 = new SqlCommand(sqlString2, scripts.GetConnection());
        int mostGames = 0;
        using (SqlDataReader reader = sqlCommand2.ExecuteReader())
        {
            while (reader.Read())
            {
                if(Convert.ToInt32(reader[0]) > mostGames)
                    mostGames = Convert.ToInt32(reader[0]);
            }
        }

        if (mostGames < 50)
            modBy = 2;
        else if (mostGames < 200)
            modBy = 10;
        else if (mostGames < 500)
            modBy = 25;
        else
            modBy = 50;
    }

    protected string GetPlayerCompareChartXML()
    {
        int maxGameNumber = 0;
        bool addGameToCategory = false;
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
        xmlDoc.AppendChild(xmlNode);
        XmlElement xEle = xmlDoc.CreateElement("graph");
        xEle.SetAttribute("showvalues", "0");
        xEle.SetAttribute("xAxisName", "Games Played");
        xEle.SetAttribute("yAxisName", statId);
        xEle.SetAttribute("decimalPrecision", "0");
        xEle.SetAttribute("rotateName", "1");
        XmlElement categories = xmlDoc.CreateElement("categories");
        XmlElement player1 = xmlDoc.CreateElement("dataset");
        player1.SetAttribute("seriesname", player1Name);
        player1.SetAttribute("color", "#660000");
        player1.SetAttribute("showAnchors", "1");
        player1.SetAttribute("anchorAlpha", "0");
        XmlElement player2 = xmlDoc.CreateElement("dataset");
        player2.SetAttribute("seriesname", player2Name);
        player2.SetAttribute("color", "#000066");
        player2.SetAttribute("showAnchors", "1");
        player2.SetAttribute("anchorAlpha", "0");
        XmlElement tmpElement;
        String sqlString = GetDataString(player1Id);
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tmpElement = xmlDoc.CreateElement("category");
                tmpElement.SetAttribute("name", Convert.ToString(reader[0]));
                if (Convert.ToUInt32(reader[0]) % modBy == 0)
                    tmpElement.SetAttribute("showName", "1");
                else
                    tmpElement.SetAttribute("showName", "0");
                categories.AppendChild(tmpElement);

                tmpElement = xmlDoc.CreateElement("set");
                tmpElement.SetAttribute("value", Convert.ToString(reader[1]));
                player1.AppendChild(tmpElement);
                maxGameNumber = Convert.ToInt32(reader[0]);
            }
        }
        String sqlString2 = GetDataString(player2Id);
        SqlCommand sqlCommand2 = new SqlCommand(sqlString2, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand2.ExecuteReader())
        {
            while (reader.Read())
            {
                tmpElement = xmlDoc.CreateElement("set");
                tmpElement.SetAttribute("value", Convert.ToString(reader[1]));
                player2.AppendChild(tmpElement);

                if(addGameToCategory || Convert.ToInt32(reader[0]) > maxGameNumber)
                {
                    tmpElement = xmlDoc.CreateElement("category");
                    tmpElement.SetAttribute("name", Convert.ToString(reader[0]));
                    if (Convert.ToUInt32(reader[0]) % modBy == 0)
                        tmpElement.SetAttribute("showName", "1");
                    else
                        tmpElement.SetAttribute("showName", "0");
                    categories.AppendChild(tmpElement);
                    addGameToCategory = true;
                }
                
            }
        }
        xEle.AppendChild(categories);
        xEle.AppendChild(player1);
        xEle.AppendChild(player2);
        xmlDoc.AppendChild(xEle);
        return xmlDoc.OuterXml;
    }

    private string GetDataString(double playerId)
    {
        return String.Format("exec avDBPlayerStatByGamesPlayed {0}, '{1}'", playerId, statId);
    }
}