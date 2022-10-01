using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth = 100;    
    public int CurHealth;
    public int Damage = 3;

    public virtual float attackInterval => 0.5f;
    private float _timer = 0;

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
}