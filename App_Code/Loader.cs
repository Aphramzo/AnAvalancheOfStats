using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

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

        String HTML = GetPageHTML(gamePage);
        HTML = HTML.Replace("<!--&nbsp;-->", "");
        HTML = HTML.Replace("&nbsp;", "");
        //lets get it into XML to make it easier to read
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.LoadXml(HTML);

        //get the game date
        System.Xml.XmlNodeList nodes = doc.SelectNodes("/html/body/table/tr/td/table/tr/td/table/tr/td/table[@id='GameInfo']");
        if (nodes.Count == 0)
            nodes = doc.SelectNodes("/XMLFile/html/body/table/tr/td/table/tr/td/table/tr/td/table[@id='GameInfo']");
        foreach (System.Xml.XmlNode node in nodes)
        {
            String dateString = node.ChildNodes[3].ChildNodes[0].InnerText;
            gameDate = Convert.ToDateTime(dateString);
        }

        nodes = doc.SelectNodes("/html/body/table/tr/td/table/tr/td");
        if (nodes.Count == 0)
           nodes = doc.SelectNodes("/XMLFile/html/body/table/tr/td/table/tr/td");
        foreach (System.Xml.XmlNode node in nodes)
        {
            if (node.InnerText.Trim() == "COLORADO AVALANCHE")
            {
                RecordPlayerGamesFromTeamTable(node.ParentNode.ParentNode, gameDate, gamePage);
                break;
            }
        }

    }

    private void RecordPlayerGamesFromTeamTable(System.Xml.XmlNode playerTable, DateTime gameDate, String gamePage)
    {
        List<PlayerSpecialTeamPoints> specialTeamPoints =  LoadSpecialTeamPoints(gamePage);
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
                RecordSinglePlayerGame(node, gameDate, specialTeamPoints);
        }
    }

    private List<PlayerSpecialTeamPoints> LoadSpecialTeamPoints(String gamePage)
    {
        List<PlayerSpecialTeamPoints> returnList = new List<PlayerSpecialTeamPoints>();
        var avGoals = 0;
        var otherGoals = 0;
        String eventPage = gamePage.Replace("ES02", "GS02");
        String HTML = GetPageHTML(eventPage);
        HTML = HTML.Replace("<!--&nbsp;-->", "");
        HTML = HTML.Replace("&nbsp;", "");
        //lets get it into XML to make it easier to read
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(HTML);

        XmlNodeList nodes = doc.SelectNodes("/html/body/table");
        if (nodes.Count == 0)
            nodes = doc.SelectNodes("/XMLFile/html/body/table");
        nodes = nodes.Item(0).ChildNodes[3].ChildNodes[0].ChildNodes[0].ChildNodes;

        foreach(XmlNode node in nodes)
        {
            try
            {
                if(node.ChildNodes[4].InnerText.Trim() == "COL" )
                {
                    //need to make sure its not a missed PS
                    if (node.ChildNodes[6].InnerText.Trim() != "Unsuccessful Penalty Shot")
                        avGoals = avGoals + 1;
                    if(node.ChildNodes[3].InnerText.Trim() == "PP" || node.ChildNodes[3].InnerText.Trim() == "PP-EN")
                    {
                        returnList.Add(new PlayerSpecialTeamPoints()
                                           {
                                               PlayerName = FormatPlayerName(node.ChildNodes[5].InnerText.Trim()),
                                               PPGoals = 1
                                           });

                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[6].InnerText.Trim()),
                            PPAssists = 1
                        });

                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[7].InnerText.Trim()),
                            PPAssists = 1
                        });
                    }
                    else if (node.ChildNodes[3].InnerText.Trim() == "SH" || node.ChildNodes[3].InnerText.Trim() == "SH-EN")
                    {
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[5].InnerText.Trim()),
                            SHGoals = 1
                        });

                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[6].InnerText.Trim()),
                            SHAssists = 1
                        });

                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[7].InnerText.Trim()),
                            SHAssists = 1
                        });
                    }

                    //lets record empty netters too, why not?
                    if(node.ChildNodes[3].InnerText.Contains("EN"))
                    {
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[5].InnerText.Trim()),
                            ENGoals = 1
                        });
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[6].InnerText.Trim()),
                            ENAssists = 1
                        });
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(node.ChildNodes[7].InnerText.Trim()),
                            ENAssists = 1
                        });
                    }
                }
                else if(node.ChildNodes[4].InnerText.Trim() != "Team")
                {
                    if (node.ChildNodes[6].InnerText.Trim() != "Unsuccessful Penalty Shot")
                        otherGoals = otherGoals + 1;
                }
            }
            catch (Exception)
            {
                
            }
        }
        //if the avs scored more, we need to grab the GWG
        if(avGoals > otherGoals)
        {
            var p = 1;
            for(var i = nodes.Count-1;i>=0;i--)
            {
                if(nodes[i].ChildNodes[4].InnerText.Trim() == "COL")
                {
                    //no GWG for stupid SO's
                    if (nodes[i].ChildNodes[1].InnerText.Trim() == "SO")
                        break;

                    
                    if (p == (avGoals-otherGoals))
                    {
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(nodes[i].ChildNodes[5].InnerText.Trim()),
                            GWGoals = 1
                        });
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(nodes[i].ChildNodes[6].InnerText.Trim()),
                            GWAssists = 1
                        });
                        returnList.Add(new PlayerSpecialTeamPoints()
                        {
                            PlayerName = FormatPlayerName(nodes[i].ChildNodes[7].InnerText.Trim()),
                            GWAssists = 1
                        });

                        break;
                    }
                    p = p + 1;
                }
            }
        }
        return returnList;
    }

    private void RecordSinglePlayerGame(XmlNode playerGame, DateTime gameDate, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        //dont want goalie stats here
        if (playerGame.ChildNodes[POSITION].InnerText.Trim() == "G")
            return;

        //If there was a team penalty (Too many men) we need to filter it out
        if (playerGame.ChildNodes[PLAYERNAME].InnerText.Trim() == "TEAM PENALTY")
            return;

        //lets make sure that this player exists in our database already
        if (!PlayerExistsInDatabase(playerGame.ChildNodes[PLAYERNAME].InnerText.Trim().Replace("''", "'")))
        {
            Console.WriteLine("need to load" + playerGame.ChildNodes[PLAYERNAME].InnerText.Trim().Replace("''", "'"));
            LoadPlayerIntoDatabase(playerGame.ChildNodes[PLAYERNAME].InnerText.Trim().Replace("''", "'"));
        }


        String sqlToExecute = "InsertPlayerGame '{0}', {1}, {2}, {3}, {4}, {5}, '{6}', {7}, '{8}', '{9}', '{10}', {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, '{20}', {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}";
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
                gameDate.ToShortDateString(),
                GetPlayerPPGoals(playerGame, specialTeamPoints),
                GetPlayerPPAssists(playerGame, specialTeamPoints),
                GetPlayerSHGoals(playerGame, specialTeamPoints),
                GetPlayerSHAssists(playerGame, specialTeamPoints),
                GetPlayerGWGoals(playerGame, specialTeamPoints),
                GetPlayerGWAssists(playerGame, specialTeamPoints),
                GetPlayerENGoals(playerGame, specialTeamPoints),
                GetPlayerENAssists(playerGame, specialTeamPoints)
            );

        SqlConnection connection = scripts.GetConnection();
        SqlCommand command = new SqlCommand(sqlToExecute, connection);
        command.ExecuteNonQuery();
        connection.Close();
    }

    private void LoadPlayerIntoDatabase(string playerName)
    {
        playerName = scripts.PlayerNameFirstLastFromLastFirst(playerName).ToUpper();
        //first we have to load the roster page
        var playerId = GetNHLPlayerIdFromName(playerName);
        var HTML = GetPageHTML(String.Format("http://avalanche.nhl.com/club/player.htm?id={0}", playerId));
        
        //since these pages are not well formatted XML, we are going to have to do this the hard way
        var position = GetHTMLInnerDiv(HTML, "plyrTmbPositionTeam\">");
        position =  position.Substring(0, position.IndexOf("-"));


        //TODO: Convert height to int, get the country from the birthplace
        var weight = GetHTMLInnerDiv(HTML, "plyrTmbStatCaption\">Weight:</span>");
        var height = GetHTMLInnerDiv(HTML, "plyrTmbStatCaption\">Height:</span>");
        var dob = GetHTMLInnerDiv(HTML, "plyrTmbStatCaption\">Born:</span>");
        dob = dob.Substring(0, dob.IndexOf("(Age"));

        var insertPlayerName = GetHTMLInnerDiv(HTML, "plyrTmbPlayerName\">");
        var birthplace = GetHTMLInnerDiv(HTML, "plyrTmbStatCaption\">Birthplace:</span>");
    }

    private String GetHTMLInnerDiv(String HTML, String whatToLookFor)
    {
        var returnString = HTML.Substring(HTML.IndexOf(whatToLookFor) + whatToLookFor.Length);
        returnString = returnString.Substring(0, returnString.IndexOf("</div"));
        returnString = scripts.CleanUglyHTMLString(returnString);
        return returnString;
    }

    private String GetNHLPlayerIdFromName(string playerName)
    {
        var HTML = GetPageHTML("http://avalanche.nhl.com/club/roster.htm").ToUpper();
        if (!HTML.Contains(playerName))
            HTML = GetPageHTML("http://avalanche.nhl.com/club/roster.htm?type=prospect").ToUpper();

        var indexOf = HTML.IndexOf(playerName + "</A>");
        var tmpID = HTML.Substring(indexOf - 13, 13);
        var id = tmpID.Split('=')[1].Split('"')[0];
        return id;
    }

    private bool PlayerExistsInDatabase(string playerName)
    {
        playerName = scripts.PlayerNameFirstLastFromLastFirst(playerName);
        var connection = scripts.GetConnection();
        var command = new SqlCommand(String.Format("select * from avDBPlayer where name = '{0}'", playerName.Replace("'","''")), connection);
        var results = command.ExecuteReader();
        if(results.HasRows)
        {
            connection.Close();
            return true;
        }

        connection.Close();
        return false;
    }

    private int GetPlayerPPGoals(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if(specialTeamPoints.Where(c=>c.PlayerName == playerName).Count() > 0 )
            return specialTeamPoints.Where(c=> c.PlayerName == playerName).Sum(x=> x.PPGoals);
        
        return 0;
    }

    private int GetPlayerPPAssists(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.PPAssists);

        return 0;
    }

    private int GetPlayerSHGoals(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.SHGoals);

        return 0;
    }

    private int GetPlayerSHAssists(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.SHAssists);

        return 0;
    }

    private int GetPlayerGWGoals(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.GWGoals);

        return 0;
    }

    private int GetPlayerGWAssists(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.GWAssists);

        return 0;
    }

    private int GetPlayerENGoals(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.ENGoals);

        return 0;
    }

    private int GetPlayerENAssists(XmlNode playerGame, List<PlayerSpecialTeamPoints> specialTeamPoints)
    {
        var playerName = GetFormattedPlayerName(playerGame);
        if (specialTeamPoints.Where(c => c.PlayerName == playerName).Count() > 0)
            return specialTeamPoints.Where(c => c.PlayerName == playerName).Sum(x => x.ENAssists);

        return 0;
    }

    private String ReadPlayerGameNode(System.Xml.XmlNode playerGame, int ordinal)
    {
        String value = playerGame.ChildNodes[ordinal].InnerText;
        if (String.IsNullOrEmpty(value))
            value = "0";

        return value.Replace("'","''").Replace("+","");
    }

    private string FormatPlayerName(string playerName)
    {
        if (String.IsNullOrEmpty(playerName) || playerName == "unassisted")
            return playerName;

        var nameArr = playerName.Split(' ');
        return nameArr[1].Split('(')[0];
    }

    private string GetFormattedPlayerName(XmlNode playerGame)
    {
        var playerName = ReadPlayerGameNode(playerGame, PLAYERNAME);
        var nameArr = playerName.Split(' ');
        playerName = String.Format("{0}.{1}", nameArr[1].Substring(0, 1), nameArr[0].Substring(0,nameArr[0].Length-1));
        return playerName.Replace("''", "'");
    }

    private static String GetPageHTML(String gamePage)
    {
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
        return HTML;
    }
}

public class PlayerSpecialTeamPoints
{
    public String PlayerName { get; set; }
    public int PPGoals { get; set; }
    public int PPAssists { get; set; }
    public int SHGoals { get; set; }
    public int SHAssists { get; set; }
    public int GWGoals { get; set; }
    public int GWAssists { get; set; }
    public int ENGoals { get; set; }
    public int ENAssists { get; set; }
}