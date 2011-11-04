using System;

public partial class LoadData : System.Web.UI.Page
{
    public Loader loader;
    public CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        loader = new Loader();
        scripts = new CommonPage();
        if (IsPostBack)
            LoadGames();
    }

    private void LoadGames()
    {
        if (!scripts.GoodPassWord(Request["pwd"]))
        {
            BadPassWordAction();
            return;
        }

       // LoadGamesResults();
        //return;
        int fromGameNumber = Convert.ToInt32(Request["from"]);
        int toGameNumber = Convert.ToInt32(Request["to"]);
        String gameNumber;
        for (int i = fromGameNumber; i <= toGameNumber; i++)
        {
            gameNumber = i.ToString().PadLeft(4, '0');
            loader.LoadPlayerGame(String.Format("http://www.nhl.com/scores/htmlreports/{0}/ES02{1}.HTM", Request["season"], gameNumber));
        }

        scripts.ExecuteMSSQLNonQuery("RegenavDBPlayerSeasonTable");
    }

    private void LoadGamesResults()
    {
        loader.LoadGameResults(String.Format("http://avalanche.nhl.com/club/gamelog.htm?season={0}&gameType=2", Request["season"]));
    }

    private void BadPassWordAction()
    {
        WrongPassword.Visible = true;
    }

}
