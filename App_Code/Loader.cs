using System;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Loader
/// </summary>
public class Loader
{
    //THESE ARE ONLY VALID FROM THE 07-08 SEASON ON
    public const int POSITION = 1;
    public const int PLAYERNAME = 2;
    public const int GOALS = 3;
    public const int ASSISTS = 4;
    public const int POINTS = 5;
    public const int PLUSMINUS = 6;
    public const int PENALTIES = 7;
    public const int PIM = 8;
    public const int TOI = 9;
    public const int SHIFTS = 10;
    public const int AVERAGESHIFTLENGTH = 11;
    public const int TOIPP = 12;
    public const int TOISH = 13;
    public const int TOIEV = 14;
    public const int SHOTS = 15;
    public const int ATTEMPTSBLOCKED = 16;
    public const int MISSEDSHOTS = 17;
    public const int HITS = 18;
    public const int GIVEAWAYS = 19;
    public const int TAKEAWAYS = 20;
    public const int BLOCKEDSHOTS = 21;
    public const int FACEOFFSWON = 22;
    public const int FACEOFFSLOST = 23;
    public const int FACEOFFPERCENT = 24;

    private CommonPage scripts;
	public Loader()
	{
        scripts = new CommonPage();
    }

    public void LoadPlayerGame(String gamePage)
    {
        DateTime gameDate = DateTime.MinValue;

        // used to build entire input
        StringBuilder sb = new StringBuilder();

        // used on each read operation
        byte[] buf = new byte[8192];

        // prepare the web page we will be asking for
        HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(gamePage);

        // execute the request
        HttpWebResponse response = (HttpWebResponse)
            request.GetResponse();

        // we will read data via the response stream
        Stream resStream = response.GetResponseStream();

        string tempString = null;
        int count = 0;

        do
        {
            // fill the buffer with data
            count = resStream.Read(buf, 0, buf.Length);

            // make sure we read some data
            if (count != 0)
            {
                // translate from bytes to ASCII text
                tempString = Encoding.ASCII.GetString(buf, 0, count);

                // continue building the string
                sb.Append(tempString);
            }
        }
        while (count > 0); // any more data to read?

        // print out page source
        String HTML = sb.ToString();
        HTML = HTML.Replace("<!--&nbsp;-->", "");
        HTML = HTML.Replace("&nbsp;", "");
        //lets get it into XML to make it easier to read
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.LoadXml(HTML);

        //get the game date
        System.Xml.XmlNodeList nodes = doc.SelectNodes("/html/body/table/tr/td/table/tr/td/table/tr/td/table[@id='GameInfo']");
        foreach (System.Xml.XmlNode node in nodes)
        {
            String dateString = node.ChildNodes[3].ChildNodes[0].InnerText;
            gameDate = Convert.ToDateTime(dateString);
        }

        nodes = doc.SelectNodes("/html/body/table/tr/td/table/tr/td");
        foreach (System.Xml.XmlNode node in nodes)
        {
            if (node.InnerText.Trim() == "COLORADO AVALANCHE")
            {
                RecordPlayerGamesFromTeamTable(node.ParentNode.ParentNode, gameDate);
                break;
            }
        }

    }

    private void RecordPlayerGamesFromTeamTable(System.Xml.XmlNode playerTable, DateTime gameDate)
    {
        Boolean startRecording = false;
        System.Xml.XmlNodeList nodes = playerTable.SelectNodes("tr");
        foreach (System.Xml.XmlNode node in nodes)
        {
            //dont start looking at stats until we are on the avs
            if (node.FirstChild.InnerText.Trim() == "COLORADO AVALANCHE")
                startRecording = true;

            //if we have already recorded stats and made it to the team totals, we are all done
            else if (node.FirstChild.InnerText.Trim() == "TEAM TOTALS" && startRecording)
                break;

            else if (startRecording && node.ChildNodes.Count > 10)
                RecordSinglePlayerGame(node, gameDate);
        }
    }

    private void RecordSinglePlayerGame(System.Xml.XmlNode playerGame, DateTime gameDate)
    {
        //dont want goalie stats here
        if (playerGame.ChildNodes[POSITION].InnerText.Trim() == "G")
            return;


        String sqlToExecute = "InsertPlayerGame '{0}', {1}, {2}, {3}, {4}, {5}, '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, '{20}'";
        sqlToExecute = String.Format(sqlToExecute,
                ReadPlayerGameNode(playerGame, PLAYERNAME),
                ReadPlayerGameNode(playerGame, GOALS),
                ReadPlayerGameNode(playerGame, ASSISTS),
                ReadPlayerGameNode(playerGame, PLUSMINUS),
                ReadPlayerGameNode(playerGame, PENALTIES),
                ReadPlayerGameNode(playerGame, PIM),
                ReadPlayerGameNode(playerGame, TOI),
                ReadPlayerGameNode(playerGame, SHIFTS),
                ReadPlayerGameNode(playerGame, TOIPP),
                ReadPlayerGameNode(playerGame, TOISH),
                ReadPlayerGameNode(playerGame, TOIEV),
                ReadPlayerGameNode(playerGame, SHOTS),
                ReadPlayerGameNode(playerGame, ATTEMPTSBLOCKED),
                ReadPlayerGameNode(playerGame, MISSEDSHOTS),
                ReadPlayerGameNode(playerGame, HITS),
                ReadPlayerGameNode(playerGame, GIVEAWAYS),
                ReadPlayerGameNode(playerGame, TAKEAWAYS),
                ReadPlayerGameNode(playerGame, BLOCKEDSHOTS),
                ReadPlayerGameNode(playerGame, FACEOFFSWON),
                ReadPlayerGameNode(playerGame, FACEOFFSLOST),
                gameDate.ToShortDateString()
            );

        SqlConnection connection = scripts.GetConnection();
        SqlCommand command = new SqlCommand(sqlToExecute, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private String ReadPlayerGameNode(System.Xml.XmlNode playerGame, int ordinal)
    {
        String value = playerGame.ChildNodes[ordinal].InnerText;
        if (String.IsNullOrEmpty(value))
            value = "0";

        return value.Replace("'","''").Replace("+","");
    }
}