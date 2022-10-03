using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{    
    public PlayerCursor PlayerCursor;
    public Transform PlayerBody;
    public Transform WeaponsOrigin;
    public PlayerParameters parameters;
    public int CurHealth { get; private set; }
    public Action OnDeath;

    public Weapon ActiveWeapon = null;
    public List<Weapon> PassiveWeapons;

    public ParticleSystem DmgPart;
    public ParticleSystem DeathPart;

    private GameManager _gm;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        CurHealth = parameters.playerMaxHealth;
        
        //AddWeapon("Fireball");
    }

    private void Update()
    {
        Movement();
        Attack();
    }

    private void Movement()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");

        var dir = new Vector3(hor, 0, vert).normalized;
        transform.position += dir * parameters.playerMovementSpeed * Time.deltaTime;

        var cursorDir = PlayerCursor.GetDirection();
        var lookDir = new Vector3(cursorDir.x, 0, cursorDir.y) * -1;
        
        if (lookDir != Vector3.zero)
        {
            PlayerBody.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var cursorDir = PlayerCursor.GetDirection();
            var lookDir = new Vector3(cursorDir.x, 0, cursorDir.y);

            if (ActiveWeapon)
            {
                ActiveWeapon.Attack(lookDir);
            }
        }

        for (int i = 0; i < PassiveWeapons.Count; i++)
        {
            PassiveWeapons[i].Attack(Vector3.zero); //TODO: get player movement dir
        }
    }

    public void AddWeapon(string wName)
    {
        var isActive = _gm.weapDB.IsActiveWeapon(wName);        
        if(isActive)
        {
            AddActiveWeapon(wName);
        }
        else
        {
            AddPassiveWeapon(wName);
        }
    }

    private void AddActiveWeapon(string wName)
    {
        if(ActiveWeapon)
        {
            var upgrade = ActiveWeapon.WeaponName == wName;
            if(upgrade)
            {
                var status = GetWeaponStatus(ActiveWeapon.WeaponName);
                Destroy(ActiveWeapon.gameObject);
                ActiveWeapon = CreateWeapon(wName, status.level + 1);
            }
            else
            {
                Destroy(ActiveWeapon.gameObject);
                ActiveWeapon = CreateWeapon(wName, 1);
            }
            return;
        }

        ActiveWeapon = CreateWeapon(wName, 1);
    }

    private void AddPassiveWeapon(string wName)
    {
        var passive = PassiveWeapons.FirstOrDefault(x => x.WeaponName == wName);
        if(!passive)
        {
            var w = CreateWeapon(wName, 1);
            PassiveWeapons.Add(w);
        }
        else
        {
            var status = GetWeaponStatus(passive.WeaponName);

            PassiveWeapons.Remove(passive);
            Destroy(passive.gameObject);

            var w = CreateWeapon(wName, status.level + 1);
            PassiveWeapons.Add(w);
        }
    }

    private Weapon CreateWeapon(string wName, int wLevel)
    {
        var pref = _gm.weapDB.FindWeaponPrefab(wName, wLevel);
        var weap = Instantiate(pref, WeaponsOrigin);
        weap.Initialize(parameters);
        return weap;
    }

    public (bool isOnPlayer, int level) GetWeaponStatus(string wName)
    {
        if(ActiveWeapon && ActiveWeapon.WeaponName == wName)
        {
            return (true, ActiveWeapon.WeaponLevel);
        }

        var passive = PassiveWeapons.FirstOrDefault(x => x.WeaponName == wName);
        if(passive != null)
        {
            return (true, passive.WeaponLevel);
        }

        return (false, 0);
    }

    public void SetDamage(int dmg)
    {
        CurHealth -= dmg;
        DmgPart.Play();
        if (CurHealth <= 0)
        {
            Instantiate(DeathPart, transform.position, Quaternion.identity);
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        CurHealth = Mathf.Clamp(CurHealth + heal, 0, parameters.playerMaxHealth);
    }
}
