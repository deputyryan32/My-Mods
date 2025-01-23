using LemonUI.Menus;
using GTA;

public class WeatherMenu
{
    public NativeMenu Menu { get; private set; }
    private MainMenu mainMenu;

    public WeatherMenu(MainMenu main)
    {
        mainMenu = main;
        Menu = new NativeMenu("Weather Options", "Select Weather");

        var clearWeather = new NativeItem("Clear");
        clearWeather.Activated += (sender, e) => WeatherHelper.SetWeather("CLEAR");
        Menu.Add(clearWeather);

        var cloudyWeather = new NativeItem("Cloudy");
        cloudyWeather.Activated += (sender, e) => WeatherHelper.SetWeather("CLOUDS");
        Menu.Add(cloudyWeather);

        var rainyWeather = new NativeItem("Rain");
        rainyWeather.Activated += (sender, e) => WeatherHelper.SetWeather("RAIN");
        Menu.Add(rainyWeather);

        var thunderWeather = new NativeItem("Thunderstorm");
        thunderWeather.Activated += (sender, e) => WeatherHelper.SetWeather("THUNDER");
        Menu.Add(thunderWeather);

        var foggyWeather = new NativeItem("Foggy");
        foggyWeather.Activated += (sender, e) => WeatherHelper.SetWeather("FOGGY");
        Menu.Add(foggyWeather);

        var snowWeather = new NativeItem("Snow");
        snowWeather.Activated += (sender, e) => WeatherHelper.SetWeather("SNOW");
        Menu.Add(snowWeather);

        var blizzardWeather = new NativeItem("Blizzard");
        blizzardWeather.Activated += (sender, e) => WeatherHelper.SetWeather("BLIZZARD");
        Menu.Add(blizzardWeather);

        var xmasWeather = new NativeItem("Christmas");
        xmasWeather.Activated += (sender, e) => WeatherHelper.SetWeather("XMAS");
        Menu.Add(xmasWeather);

        // Back option to return to the main menu
        var backItem = new NativeItem("Back");
        backItem.Activated += (sender, e) =>
        {
            Menu.Visible = false;
            mainMenu.Show();
        };
        Menu.Add(backItem);
    }
}
