using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoalieGame
/// </summary>
public class GoalieGame
{
    public Int64 Id { get; set; }
    public App_Code.Player Player { get; set; }
    public Game Game { get; set; }
    public bool Win { get; set; }
    public bool Loss { get; set; }
    public bool Tie { get; set; }
    public bool OT { get; set; }
    public bool ShootOut { get; set; }
    public int ShotAgaisnt { get; set; }
    public int Saves { get; set; }
    public int GoalsAgainst { get; set; }
    public bool Shutout { get; set; }
    public int Goals { get; set; }
    public int Assists { get; set; }
    public int SOG { get; set; }
    public int PIM { get; set; }
    public int TimeOnIce { get; set; }
}
