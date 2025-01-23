using LemonUI.Menus;
using GTA;

public class TimeMenu
{
    public NativeMenu Menu { get; private set; }
    private MainMenu mainMenu;

    public TimeMenu(MainMenu main)
    {
        mainMenu = main;
        Menu = new NativeMenu("Time Options", "Select Time");

        var earlyMorning = new NativeItem("Early Morning (5:00 AM)");
        earlyMorning.Activated += (sender, e) => TimeHelper.SetTime(5, 0, 0);
        Menu.Add(earlyMorning);

        var morning = new NativeItem("Morning (9:00 AM)");
        morning.Activated += (sender, e) => TimeHelper.SetTime(9, 0, 0);
        Menu.Add(morning);

        var noon = new NativeItem("Noon (12:00 PM)");
        noon.Activated += (sender, e) => TimeHelper.SetTime(12, 0, 0);
        Menu.Add(noon);

        var afternoon = new NativeItem("Afternoon (3:00 PM)");
        afternoon.Activated += (sender, e) => TimeHelper.SetTime(15, 0, 0);
        Menu.Add(afternoon);

        var evening = new NativeItem("Evening (6:00 PM)");
        evening.Activated += (sender, e) => TimeHelper.SetTime(18, 0, 0);
        Menu.Add(evening);

        var night = new NativeItem("Night (9:00 PM)");
        night.Activated += (sender, e) => TimeHelper.SetTime(21, 0, 0);
        Menu.Add(night);

        var midnight = new NativeItem("Midnight (12:00 AM)");
        midnight.Activated += (sender, e) => TimeHelper.SetTime(0, 0, 0);
        Menu.Add(midnight);

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
