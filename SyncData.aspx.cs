using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;

public partial class SyncData : System.Web.UI.Page
{
    protected CommonPage Scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        Scripts = new CommonPage();
        testConnection();
    }

    private void testConnection()
    {
        var helper = new PlayerHelper();
        var hisPlayers = helper.GetPlayerListFromMySQL();
        var myPlayers = helper.GetPlayerListFromMSSQL();
        foreach(var player in myPlayers)
        {
            var hisPlayer = hisPlayers.Where(c => c.Id == player.Id).FirstOrDefault();
            if(hisPlayer == null)
            {
                helper.AddPlayerToMySQL(player);
                Response.Write(String.Format("Adding player {0} {1}", player.Name, "<br />"));
            }
            else if(hisPlayer.IsCurrent != player.IsCurrent)
            {
                hisPlayer.IsCurrent = player.IsCurrent;
                helper.UpdateMySQLPlayer(hisPlayer);

                Response.Write(String.Format("Updating {0}'s IsCurrent to {1} {2}", player.Name, player.IsCurrent.ToString(), "<br />"));
            }

        }

    }
}
