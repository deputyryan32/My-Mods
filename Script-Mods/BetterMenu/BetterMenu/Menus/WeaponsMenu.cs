using LemonUI.Menus;
using GTA;
using System;

public class WeaponsMenu
{
    public NativeMenu Menu { get; private set; }
    private MainMenu mainMenu;

    // Submenus for weapon categories
    private NativeMenu pistolsMenu;
    private NativeMenu riflesMenu;
    private NativeMenu shotgunsMenu;
    private NativeMenu smgMenu;
    private NativeMenu heavyMenu;

    public WeaponsMenu(MainMenu main)
    {
        mainMenu = main;
        Menu = new NativeMenu("Weapons Menu", "Select Weapon Category");

        // Weapon categories
        AddWeaponCategory("Pistols", ShowPistolOptions);
        AddWeaponCategory("Rifles", ShowRifleOptions);
        AddWeaponCategory("Shotguns", ShowShotgunOptions);
        AddWeaponCategory("SMGs", ShowSMGOptions);
        AddWeaponCategory("Heavy Weapons", ShowHeavyOptions);

        // Back option to return to the main menu
        var backItem = new NativeItem("Back");
        backItem.Activated += (sender, e) =>
        {
            Menu.Visible = false;
            mainMenu.Show();
        };
        Menu.Add(backItem);
    }

    private void AddWeaponCategory(string name, Action showOptions)
    {
        var categoryItem = new NativeItem(name);
        categoryItem.Activated += (sender, e) =>
        {
            Menu.Visible = false; // Hide main weapons menu
            showOptions();
        };
        Menu.Add(categoryItem);
    }

    private void ShowPistolOptions()
    {
        if (pistolsMenu == null)
        {
            pistolsMenu = new NativeMenu("Pistols", "Select a Pistol");
            AddWeaponWithAttachments(pistolsMenu, "Pistol", "WEAPON_PISTOL");
            AddWeaponWithAttachments(pistolsMenu, "Combat Pistol", "WEAPON_COMBATPISTOL");
            AddAmmoFillingOption(pistolsMenu);

            // Back button to return to main Weapons menu
            var backItem = new NativeItem("Back");
            backItem.Activated += (sender, e) =>
            {
                pistolsMenu.Visible = false;
                Menu.Visible = true;
            };
            pistolsMenu.Add(backItem);
        }

        pistolsMenu.Visible = true;
    }

    private void ShowRifleOptions()
    {
        if (riflesMenu == null)
        {
            riflesMenu = new NativeMenu("Rifles", "Select a Rifle");
            AddWeaponWithAttachments(riflesMenu, "Carbine Rifle", "WEAPON_CARBINERIFLE");
            AddAmmoFillingOption(riflesMenu);

            var backItem = new NativeItem("Back");
            backItem.Activated += (sender, e) =>
            {
                riflesMenu.Visible = false;
                Menu.Visible = true;
            };
            riflesMenu.Add(backItem);
        }

        riflesMenu.Visible = true;
    }

    private void ShowShotgunOptions()
    {
        if (shotgunsMenu == null)
        {
            shotgunsMenu = new NativeMenu("Shotguns", "Select a Shotgun");
            AddWeaponWithAttachments(shotgunsMenu, "Pump Shotgun", "WEAPON_PUMPSHOTGUN");
            AddAmmoFillingOption(shotgunsMenu);

            var backItem = new NativeItem("Back");
            backItem.Activated += (sender, e) =>
            {
                shotgunsMenu.Visible = false;
                Menu.Visible = true;
            };
            shotgunsMenu.Add(backItem);
        }

        shotgunsMenu.Visible = true;
    }

    private void ShowSMGOptions()
    {
        if (smgMenu == null)
        {
            smgMenu = new NativeMenu("SMGs", "Select an SMG");
            AddWeaponWithAttachments(smgMenu, "Micro SMG", "WEAPON_MICROSMG");
            AddAmmoFillingOption(smgMenu);

            var backItem = new NativeItem("Back");
            backItem.Activated += (sender, e) =>
            {
                smgMenu.Visible = false;
                Menu.Visible = true;
            };
            smgMenu.Add(backItem);
        }

        smgMenu.Visible = true;
    }

    private void ShowHeavyOptions()
    {
        if (heavyMenu == null)
        {
            heavyMenu = new NativeMenu("Heavy Weapons", "Select a Heavy Weapon");
            AddWeaponWithAttachments(heavyMenu, "RPG", "WEAPON_RPG");
            AddAmmoFillingOption(heavyMenu);

            var backItem = new NativeItem("Back");
            backItem.Activated += (sender, e) =>
            {
                heavyMenu.Visible = false;
                Menu.Visible = true;
            };
            heavyMenu.Add(backItem);
        }

        heavyMenu.Visible = true;
    }

    private void AddWeaponWithAttachments(NativeMenu menu, string weaponName, string weaponHash)
    {
        var weaponItem = new NativeItem(weaponName);
        weaponItem.Activated += (sender, e) => WeaponsHelper.GiveWeapon(weaponHash);
        menu.Add(weaponItem);

        var suppressorItem = new NativeItem($"Attach Suppressor to {weaponName}");
        suppressorItem.Activated += (sender, e) => WeaponsHelper.AttachSuppressor(weaponHash);
        menu.Add(suppressorItem);
    }

    private void AddAmmoFillingOption(NativeMenu menu)
    {
        var fullAmmoItem = new NativeItem("Fill Ammo");
        fullAmmoItem.Activated += (sender, e) => WeaponsHelper.FillAmmo();
        menu.Add(fullAmmoItem);
    }
}
