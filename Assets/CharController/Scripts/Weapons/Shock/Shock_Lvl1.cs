using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Lvl1 : Weapon
{
    public override float reloadSpeed => 1.5f;    
    public virtual int damage => 5;

    public Electric electric;
    public Transform pointer;
    protected Enemy enem;
    protected bool lockOnTarget = false;    

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        if(enem)
        {            
            lockOnTarget = true;
            yield return new WaitForSeconds(0.3f);
            enem.SetDamage((int)(damage * _parameters.baseDamage));
            lockOnTarget = false;            
        }
        
        yield break;
    }

    private void Update()
    {
        if(lockOnTarget && enem)
        {
            pointer.transform.position = enem.transform.position;
        }
        else
        {
            pointer.transform.localPosition = new Vector3(0, 2, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!lockOnTarget)
        {
            var en = other.GetComponent<Enemy>();
            if (en)
            {
                enem = en;
            }
        }
    }
}
