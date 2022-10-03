using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite_Lvl1 : Weapon
{
    public Transform Orb;
    public virtual int damage => 5;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
        Orb.transform.localPosition = Vector3.Lerp(Orb.transform.localPosition, new Vector3(0, 0, 3), 0.1f);
    }

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        yield break;
    }

    public void OnTrigger(Collider other)
    {
        var en = other.GetComponent<Enemy>();
        if (en)
        {
            en.SetDamage((int)(damage * _parameters.baseDamage));
        }
    }
}
