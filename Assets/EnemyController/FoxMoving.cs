using System.Collections;
using UnityEngine;
public class FoxMoving : MovingToPlayerEnemy
{
    [SerializeField]
    float startMoveSpeed = 0.5f, sprintMoveSpeed = 7f, timeToSprint = 5f;
    protected override void OnStart()
    {
        base.OnStart();
        MoveSpeed = startMoveSpeed;
        StartCoroutine(WaitToSprint());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnAttack()
    {
        base.OnAttack();
        MoveSpeed = startMoveSpeed;
        StartCoroutine(WaitToSprint());
    }

    public override void SetDamage(int dmg)
    {
        base.SetDamage(dmg);
        Corrupt();
    }

    IEnumerator WaitToSprint()
    {
        yield return new WaitForSeconds(timeToSprint);
        MoveSpeed = sprintMoveSpeed;
    }

}
