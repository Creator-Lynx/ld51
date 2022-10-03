using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string WeaponName;
    public int WeaponLevel;

    protected PlayerParameters _parameters;
    public bool Ready { get; private set; } = true;

    public virtual float reloadSpeed => 1f;

    private Coroutine _curAttack = null;

    public void Initialize(PlayerParameters parameters)
    {
        _parameters = parameters;
    }

    public void Attack(Vector3 attackDir)
    {
        if (_curAttack == null)
        {
            Ready = false;
            _curAttack = StartCoroutine(AttackProcess(attackDir));
        }
    }

    private IEnumerator AttackProcess(Vector3 attackDir)
    {
        yield return StartCoroutine(OnAttack(attackDir));
        yield return new WaitForSeconds(reloadSpeed * _parameters.baseReloadSpeed);
        _curAttack = null;
        Ready = true;
    }

    protected abstract IEnumerator OnAttack(Vector3 attackDir);
}
