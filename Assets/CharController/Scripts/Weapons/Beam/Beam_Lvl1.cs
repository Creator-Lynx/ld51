using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Lvl1 : Weapon
{
    public ParticleSystem BeamParticle;
    public Collider BeamCollider;

    public override float reloadSpeed => 1;
    public virtual int damage => 20;

    private List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        BeamCollider.enabled = false;
    }

    protected override IEnumerator OnAttack(Vector3 attackDir)
    {
        transform.rotation = Quaternion.LookRotation(-attackDir);
        BeamParticle.Play();
        yield return new WaitForSeconds(1.1f);
        BeamCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        //yield return new WaitForEndOfFrame();
        BeamCollider.enabled = false;
        yield return new WaitForSeconds(0.55f);
    }

    private void Damage()
    {
        Debug.Log(enemies.Count);
        foreach(var e in enemies)
        {
            e.SetDamage((int)(damage * _parameters.baseDamage));
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        var en = other.GetComponent<Enemy>();
        if (en)
        {            
            en.SetDamage((int)(damage * _parameters.baseDamage));
        }
    }
}
