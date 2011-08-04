using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PlayerGame
/// </summary>
public class PlayerGame
{
    public Int64 Id { get; set; }
    public App_Code.Player Player { get; set; }
    public Game Game { get; set; }
    public Statistics Stats { get; set; }

}
