using LemonUI.Menus;
using GTA;

public class MainMenu
{
    public NativeMenu Menu { get; private set; }

    public WeatherMenu weatherMenu;
    public TimeMenu timeMenu;

    public MainMenu()
    {
        Menu = new NativeMenu("BetterMenu", "Main Menu");

        // Initialize submenus and pass reference to MainMenu for back navigation
        weatherMenu = new WeatherMenu(this);
        timeMenu = new TimeMenu(this);

        // Weather Options
        var weatherMenuItem = new NativeItem("Weather Options");
        weatherMenuItem.Activated += (sender, e) =>
        {
            Menu.Visible = false;
            weatherMenu.Menu.Visible = true;
        };
        Menu.Add(weatherMenuItem);

        // Time Options
        var timeMenuItem = new NativeItem("Time Options");
        timeMenuItem.Activated += (sender, e) =>
        {
            Menu.Visible = false;
            timeMenu.Menu.Visible = true;
        };
        Menu.Add(timeMenuItem);
    }

    // Method to show the main menu
    public void Show()
    {
        Menu.Visible = true;
    }
}
