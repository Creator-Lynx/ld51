using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Weapon DB", fileName = "New DB")]
public class WeaponsDataBase : ScriptableObject
{
    public WeaponsDataBaseRecord[] records;

    public Weapon FindWeaponPrefab(string wName, int wLevel)
    {
        return records.First(x => x.Name == wName).FindWeaponPrefab(wLevel);
    }

    public bool IsActiveWeapon(string wName)
    {
        return records.First(x => x.Name == wName).IsActive;
    }

    [ContextMenu("Set Weapon Names and Levels")]
    public void SetNamesAndLevels()
    {
        foreach(var record in records)
        {
            record.SetNamesAndLevels();
        }
    }

    public Sprite GetWeaponIcon(string wName)
    {
        return records.First(x => x.Name == wName).WeaponIcon;
    }

    public string GetDescription(string wName, int wLevel)
    {
        return records.First(x => x.Name == wName).GetDescription(wLevel);
    }

    public int GetMaxLevel(string wName)
    {        
        return records.First(x => x.Name == wName).Prefabs.Count();
    }
}
