using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerParameters
{
    public int playerMaxHealth = 100;
    public float playerMovementSpeed = 5f;
    public int additionProjectilesCount = 0;

    public float baseDamageRange = 1f;
    public float baseAttackSpeed = 1f;
    public float baseAttackDuration = 1f;
    public float baseReloadSpeed = 1f;
}
