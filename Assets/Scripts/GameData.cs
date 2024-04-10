using System;
using System.Collections.Generic;

[Serializable]
public struct GameData
{
    public float HP;
    public WeaponManager CurrentWeapon;
    public ItemManager CurrentHelmet;
    public ItemManager CurrentJacket;
    public ItemManager CurrentPants;
    public List<ItemManager> Items;
}