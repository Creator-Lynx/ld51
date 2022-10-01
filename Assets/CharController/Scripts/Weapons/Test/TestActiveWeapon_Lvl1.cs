using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveWeapon_Lvl1 : Weapon
{
    public override float reloadSpeed => 0.5f;

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        Debug.Log($"Attack {reloadSpeed}");
        yield return null;
    }
}
