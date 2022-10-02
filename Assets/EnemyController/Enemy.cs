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

    public Material CorruptedMat;
    public bool IsCorrupted { get; private set; } = false;
    public Action OnCorrupted;

    private void Start()
    {
        CurHealth = MaxHealth;
        OnStart();
    }

    protected virtual void OnStart() { }

    private void Update()
    {
        _timer -= Time.deltaTime;
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

    public void SetDamage(int dmg)
    {
        CurHealth -= dmg;
        if(CurHealth <= 0)
        {
            FindObjectOfType<GameManager>().AddScore(KillScore);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && _timer <= 0)
        {
            collision.collider.GetComponent<PlayerController>().SetDamage(Damage);
            _timer = attackInterval;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
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
            IsCorrupted = true;
            GetComponentInChildren<Animator>().SetBool("IsEvil", true);
            OnCorrupted?.Invoke();            
        }
    }
}
