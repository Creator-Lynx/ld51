using System.Collections;
using UnityEngine;

public class FrogBehavior : Enemy
{
    public override float attackInterval => 0.2f;
    private Transform Player;
    public float MoveSpeed = 10f;
    [SerializeField]
    float movingDelay = 7f, corruptAnimDelay = 1.2f;
    [SerializeField]
    ParticleSystem explosion;

    protected override void OnStart()
    {
        base.OnStart();
        isMakingDamage = false;
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(MovingDelay());
    }
    protected override void OnUpdate()
    {
        if (Player)
        {
            var dir = (Player.position - transform.position).normalized;
            dir = new Vector3(dir.x, 0, dir.z);
            transform.position += dir * MoveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
    protected override void OnAttack()
    {
        base.OnAttack();
    }

    IEnumerator MovingDelay()
    {
        yield return new WaitForSeconds(movingDelay);
        isTakingDamage = false;
        Corrupt();
        yield return new WaitForSeconds(corruptAnimDelay);
        isMakingDamage = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.5f);
    }

}
