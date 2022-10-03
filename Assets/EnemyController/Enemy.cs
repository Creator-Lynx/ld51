using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurHealth;
    public int Damage = 3;
    public int KillScore = 10;

    public virtual float attackInterval => 0.5f;
    private float _timer = 0;

    public bool IsCorrupted { get; private set; } = false;
    public Action OnCorrupted;

    public ParticleSystem CorruptedPart;
    public ParticleSystem DmgPart;
    public ParticleSystem DeathPart;

    protected GameManager _manager;
    protected bool isTakingDamage = true;
    protected bool isMakingDamage = true;
    protected bool isDead = false;

    private void Start()
    {
        _manager = FindObjectOfType<GameManager>();
        CurHealth = MaxHealth;
        OnStart();
    }

    protected virtual void OnStart() { }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (CurHealth <= 0) isDead = true;
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

    public virtual void SetDamage(int dmg)
    {
        if (isDead) return;
        CurHealth -= dmg;
        DmgPart.Play();
        //Corrupt();
        if (CurHealth <= 0)
        {
            Instantiate(DeathPart, transform.position, Quaternion.identity);
            FindObjectOfType<GameManager>().AddScore(KillScore);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isMakingDamage)
            if (collision.collider.tag == "Player" && _timer <= 0)
            {
                collision.collider.GetComponent<PlayerController>().SetDamage(Damage);
                _timer = attackInterval;
                OnAttack();

            }
    }

    protected virtual void OnAttack() { }

    private void OnCollisionStay(Collision collision)
    {
        if (isMakingDamage)
            if (collision.collider.tag == "Player" && _timer <= 0)
            {
                collision.collider.GetComponent<PlayerController>().SetDamage(Damage);
                _timer = attackInterval;
            }
    }

    public void Corrupt()
    {
        if (!IsCorrupted)
        {
            CorruptedPart.Play();
            IsCorrupted = true;
            GetComponentInChildren<Animator>().SetBool("IsEvil", true);
            OnCorrupted?.Invoke();
        }
    }
}
