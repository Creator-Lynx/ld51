using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public int _dmg = 5;
    private float _speed = 1f;    

    public void Initialize(float speed, int dmg)
    {
        _dmg = dmg;
        _speed = speed;
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var en = collision.collider.GetComponent<Enemy>();
        if(en)
        {
            en.SetDamage(_dmg);
            Destroy(gameObject);
        }
    }
}
