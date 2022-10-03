using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Lvl2 : Shock_Lvl1
{
    public override float reloadSpeed => 1.3f;
    //public  int damage = 3;

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        if (enem/* && enem.CurHealth > 0*/)
        {
            lockOnTarget = true;
            yield return new WaitForSeconds(0.3f);
            if (enem.CurHealth > 0)
            {
                enem.SetDamage((int)(damage * _parameters.baseDamage));
            }
            yield return new WaitForEndOfFrame();
        }

        if (enem /*&& enem.CurHealth > 0*/)
        {
            lockOnTarget = true;
            yield return new WaitForSeconds(0.3f);
            if (enem.CurHealth > 0)
            {
                enem.SetDamage((int)(damage * _parameters.baseDamage));
            }
            yield return new WaitForEndOfFrame();
        }

        if (enem/* && enem.CurHealth > 0*/)
        {
            lockOnTarget = true;
            yield return new WaitForSeconds(0.3f);
            if (enem.CurHealth > 0)
            {
                enem.SetDamage((int)(damage * _parameters.baseDamage));
            }
        }

        lockOnTarget = false;
        yield break;
    }
}
