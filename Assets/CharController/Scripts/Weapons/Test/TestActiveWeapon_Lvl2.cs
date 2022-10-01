using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActiveWeapon_Lvl2 : TestActiveWeapon_Lvl1
{
    public override float reloadSpeed => base.reloadSpeed * 0.9f;

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        Debug.Log($"Attack 2 {reloadSpeed}");
        yield return null;
    }
}
