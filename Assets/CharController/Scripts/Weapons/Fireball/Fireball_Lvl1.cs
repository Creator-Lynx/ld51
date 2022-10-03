using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Lvl1 : Weapon
{
    public FireballProjectile projectile;

    public override float reloadSpeed => 0.3f;
    protected virtual float projectileSpeed => 15f;
    public virtual int damage => 5;    

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        var pr = Instantiate(projectile, transform.position, Quaternion.LookRotation(attackDir));
        pr.Initialize(projectileSpeed, (int)(damage * _parameters.baseDamage));
        pr.transform.rotation = Quaternion.LookRotation(attackDir);
        yield break;
    }
}
