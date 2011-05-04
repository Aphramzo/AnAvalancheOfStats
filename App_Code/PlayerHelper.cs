using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App_Code;

/// <summary>
/// Summary description for PlayerHelper
/// </summary>
public class PlayerHelper
{
    private CommonPage Scripts;
	public PlayerHelper()
	{
        Scripts = new CommonPage();
	}

    public List<Player> GetPlayerListFromMySQL()
    {
        var avDBplayerList = new List<App_Code.Player>();
        var connection = Scripts.GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = "select * from avDBPlayer";
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            avDBplayerList.Add(new App_Code.Player()
            {
                Id = Reader.GetInt64(0),
                Name = Reader.GetString(1),
                DOB = Reader.GetDateTime(2),
                CountryId = Reader.GetInt32(3),
                Height = Reader.GetInt32(4),
                Weight = Reader.GetInt32(5),
                PositionId = Reader.GetInt32(6),
                IsCurrent = Reader.GetBoolean(7)

            });
        }
        connection.Close();
        return avDBplayerList;
    }

    public List<Player> GetPlayerListFromMSSQL()
    {
        var avDBplayerList = new List<App_Code.Player>();
        var connection = Scripts.GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = "select * from avDBPlayer";
        var Reader = command.ExecuteReader();
        while (Reader.Read())
        {
            avDBplayerList.Add(new App_Code.Player()
            {
                Id = Convert.ToInt64(Reader.GetValue(0)),
                Name = Reader.GetValue(1).ToString(),
                DOB = Convert.ToDateTime(Reader.GetValue(2)),
                CountryId = Convert.ToInt32(Reader.GetValue(3)),
                Height = Convert.ToInt32(Reader.GetValue(4)),
                Weight = Convert.ToInt32(Reader.GetValue(5)),
                PositionId = Convert.ToInt32(Reader.GetValue(6)),
                IsCurrent = Convert.ToBoolean(Reader.GetValue(7))

            });
        }
        connection.Close();
        return avDBplayerList;
    }

    public void UpdateMySQLPlayer(Player player)
    {
        var sqlToExecute = String.Format("update avDBPlayer set IsCurrent = {0} where id = {1}", player.IsCurrent, player.Id);
        Scripts.ExecuteMySQLNonQuery(sqlToExecute);
    }

    public void AddPlayerToMySQL(Player player)
    {
        var sqlToExecute =
            String.Format(
                "insert into avDBPlayer (Id, Name, DOB, CountryId, Height, Weight, PositionId, IsCurrent) Values ({0},'{1}','{2}',{3},{4},{5},{6},{7})", 
                player.Id,
                player.Name.Replace("'","''"),
                Scripts.DateToMySQLDate(player.DOB),
                player.CountryId,
                player.Height,
                player.Weight,
                player.PositionId,
                player.IsCurrent
                );

        Scripts.ExecuteMySQLNonQuery(sqlToExecute);
    }
}
