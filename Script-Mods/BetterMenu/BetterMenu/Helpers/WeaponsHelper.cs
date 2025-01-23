using GTA;
using GTA.Native;

public static class WeaponsHelper
{
    public static void GiveWeapon(string weaponHash)
    {
        Function.Call(Hash.GIVE_WEAPON_TO_PED, Game.Player.Character, Function.Call<int>(Hash.GET_HASH_KEY, weaponHash), 999, false, true);
    }

    public static void AttachSuppressor(string weaponHash)
    {
        uint suppressorComponent = 0;

        switch (weaponHash)
        {
            case "WEAPON_PISTOL":
                suppressorComponent = unchecked((uint)WeaponComponent.COMPONENT_AT_PI_SUPP);
                break;
            case "WEAPON_ASSAULTRIFLE":
                suppressorComponent = unchecked((uint)WeaponComponent.COMPONENT_AT_AR_SUPP_02);
                break;
        }

        if (suppressorComponent != 0)
        {
            Function.Call(Hash.GIVE_WEAPON_COMPONENT_TO_PED, Game.Player.Character, Function.Call<int>(Hash.GET_HASH_KEY, weaponHash), suppressorComponent);
        }
    }

    public static void AttachExtendedMag(string weaponHash)
    {
        uint extendedMagComponent = 0;

        switch (weaponHash)
        {
            case "WEAPON_PISTOL":
                extendedMagComponent = unchecked((uint)WeaponComponent.COMPONENT_PISTOL_CLIP_02);
                break;
            case "WEAPON_ASSAULTRIFLE":
                extendedMagComponent = unchecked((uint)WeaponComponent.COMPONENT_ASSAULTRIFLE_CLIP_02);
                break;
        }

        if (extendedMagComponent != 0)
        {
            Function.Call(Hash.GIVE_WEAPON_COMPONENT_TO_PED, Game.Player.Character, Function.Call<int>(Hash.GET_HASH_KEY, weaponHash), extendedMagComponent);
        }
    }

    public static void AttachScope(string weaponHash)
    {
        uint scopeComponent = 0;

        switch (weaponHash)
        {
            case "WEAPON_ASSAULTRIFLE":
                scopeComponent = unchecked((uint)WeaponComponent.COMPONENT_AT_SCOPE_MACRO);
                break;
            case "WEAPON_SNIPERRIFLE":
                scopeComponent = unchecked((uint)WeaponComponent.COMPONENT_AT_SCOPE_MEDIUM);
                break;
        }

        if (scopeComponent != 0)
        {
            Function.Call(Hash.GIVE_WEAPON_COMPONENT_TO_PED, Game.Player.Character, Function.Call<int>(Hash.GET_HASH_KEY, weaponHash), scopeComponent);
        }
    }

    public static void FillAmmo()
    {
        Function.Call(Hash.SET_PED_AMMO, Game.Player.Character, Game.Player.Character.Weapons.Current.Hash, 9999);
    }
}

// Define WeaponComponent manually with unchecked for large values
public enum WeaponComponent : uint
{
    COMPONENT_AT_PI_SUPP = unchecked((uint)0xC304849A),
    COMPONENT_AT_AR_SUPP_02 = unchecked((uint)0xA73D4664),
    COMPONENT_PISTOL_CLIP_02 = unchecked((uint)0xED265A1C),
    COMPONENT_ASSAULTRIFLE_CLIP_02 = unchecked((uint)0xB1214F9B),
    COMPONENT_AT_SCOPE_MACRO = unchecked((uint)0x9D2FBF29),
    COMPONENT_AT_SCOPE_MEDIUM = unchecked((uint)0xA0D89C42),
}
