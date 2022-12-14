using UnityEngine;

public class MovingToPlayerEnemy : Enemy
{
    private Transform Player;
    public float MoveSpeed = 5f;

    protected override void OnStart()
    {
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void OnUpdate()
    {
        if (Player)
        {
            var dir = (Player.position - transform.position).normalized;
            dir = new Vector3(dir.x, 0, dir.z);
            transform.position += dir * MoveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);

            if (Vector3.Distance(transform.position, Player.position) < 9f)
            {
                Corrupt();
            }
            if (base._manager.IsActivePhase)
            {
                //Corrupt();
            }
        }
    }
    public override void SetDamage(int dmg)
    {
        base.SetDamage(dmg);
        Corrupt();
    }
}
