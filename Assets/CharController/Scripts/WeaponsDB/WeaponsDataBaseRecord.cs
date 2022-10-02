using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class WeaponsDataBaseRecord
{
    public string Name;
    public bool IsActive;
    public Sprite WeaponIcon;
    public WeaponLevelPrefab[] Prefabs;

    public Weapon FindWeaponPrefab(int wLevel)
    {
        return Prefabs.First(x => x.Level == wLevel).Prefab;
    }

    public void SetNamesAndLevels()
    {
        foreach(var weapon in Prefabs)
        {
            weapon.Prefab.WeaponName = Name;
            weapon.Prefab.WeaponLevel = weapon.Level;
        }
    }

    public string GetDescription(int wLevel)
    {
        return Prefabs.First(x => x.Level == wLevel).Description;
    }
}
