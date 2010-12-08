using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class PlayerChart : System.Web.UI.Page
{
    private CommonPage scripts;
    private double playerId;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        playerId = Convert.ToDouble(Request["Id"]);
    }

    protected string GetPlayerChartXML()
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlNode xmlNode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
        xmlDoc.AppendChild(xmlNode);
        XmlElement xEle = xmlDoc.CreateElement("graph");
        xEle.SetAttribute("showvalues", "0");
        XmlElement categories = xmlDoc.CreateElement("categories");
        XmlElement pointsPerGame = xmlDoc.CreateElement("dataset");
        pointsPerGame.SetAttribute("seriesname", "Points per Game");
        pointsPerGame.SetAttribute("color", "#000000");
        XmlElement assistsPerGame = xmlDoc.CreateElement("dataset");
        assistsPerGame.SetAttribute("seriesname", "Assists per Game");
        assistsPerGame.SetAttribute("color", "#000066");
        XmlElement goalsPerGame = xmlDoc.CreateElement("dataset");
        goalsPerGame.SetAttribute("seriesname", "Goals per Game");
        goalsPerGame.SetAttribute("color", "#660000");
        XmlElement tmpElement;
        String sqlString = GetDataString();
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                tmpElement = xmlDoc.CreateElement("category");
                tmpElement.SetAttribute("name", Convert.ToString(reader[3]));          
                categories.AppendChild(tmpElement);
                
                tmpElement = xmlDoc.CreateElement("set");
                tmpElement.SetAttribute("value", Convert.ToString(reader[2]));
                pointsPerGame.AppendChild(tmpElement);
                
                tmpElement = xmlDoc.CreateElement("set");
                tmpElement.SetAttribute("value", Convert.ToString(reader[1]));
                goalsPerGame.AppendChild(tmpElement);
                
                tmpElement = xmlDoc.CreateElement("set");
                tmpElement.SetAttribute("value", Convert.ToString(reader[0]));
                assistsPerGame.AppendChild(tmpElement);
            }
        }
        xEle.AppendChild(categories);
        xEle.AppendChild(pointsPerGame);
        xEle.AppendChild(assistsPerGame);
        xEle.AppendChild(goalsPerGame);
        xmlDoc.AppendChild(xEle);
        return xmlDoc.OuterXml;
    }

    private string GetDataString()
    {
        if(String.IsNullOrEmpty(Request["Playoffs"]))
            return String.Format("select {0} from vPlayerSeason {1} order by OrderNumber", "AssistsPerGame, GoalsPerGame, PointsPerGame, SeasonName", " where playerId = " + playerId);
        else
            return String.Format("select {0} from vPlayerPlayoff {1} order by OrderNumber", "AssistsPerGame, GoalsPerGame, PointsPerGame, SeasonName", " where playerId = " + playerId);
    }
}
