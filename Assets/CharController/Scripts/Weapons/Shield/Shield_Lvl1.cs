using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Lvl1 : Weapon
{
    private List<Enemy> enemies = new List<Enemy>();

    public override float reloadSpeed => 0.8f;
    public int damage = 5;

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            var en = enemies[i];
            if (en && en.CurHealth > 0)
            {
                en.SetDamage((int)(damage * _parameters.baseDamage));
            }
            else
            {
                enemies.Remove(en);
            }
        }

        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        var en = other.GetComponent<Enemy>();
        //en?.SetDamage((int)(damage * _parameters.baseDamage));
        if (en)
        {
            enemies.Add(en);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var en = other.GetComponent<Enemy>();
        if (en)
        {
            enemies.Remove(en);
        }
    }
}
