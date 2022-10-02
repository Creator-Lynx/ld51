using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponLevelPrefab
{
    public int Level = 1;
    public Weapon Prefab;
    [Multiline(3)] public string Description;
}
