using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoadData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Loader loader = new Loader();
        loader.LoadPlayerGame("http://www.nhl.com/scores/htmlreports/20102011/ES020811.HTM");
    }
}
