using System;

namespace App_Code
{
    /// <summary>
    /// Summary description for Player
    /// </summary>
    public class Player
    {
        public Int64 Id { get; set; }
        public String Name { get; set; }
        public DateTime DOB { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public Boolean IsCurrent { get; set; }
        //make these class references later
        public int CountryId { get; set; }
        public int PositionId { get; set; }

    }
}
