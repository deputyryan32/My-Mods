using GTA;
using LemonUI;
using System;
using System.Windows.Forms;

public class BetterMenu : Script
{
    private ObjectPool pool;
    private MainMenu mainMenu;

    public BetterMenu()
    {
        pool = new ObjectPool();
        mainMenu = new MainMenu();

        // Add menus to the pool
        pool.Add(mainMenu.Menu);
        pool.Add(mainMenu.weatherMenu.Menu);
        pool.Add(mainMenu.timeMenu.Menu);

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnTick(object sender, EventArgs e)
    {
        pool.Process();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.M)
        {
            if (!pool.AreAnyVisible)
            {
                mainMenu.Show();
            }
            else
            {
                pool.HideAll();
            }
        }
    }
}
