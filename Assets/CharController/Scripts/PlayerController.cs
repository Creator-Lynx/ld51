using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public PlayerCursor PlayerCursor;
    public Transform WeaponsOrigin;
    public PlayerParameters parameters;
    public int CurHealth { get; private set; }

    public Weapon ActiveWeapon = null;
    public List<Weapon> PassiveWeapons;

    private WeaponsDataBase weapDB;

    private void Start()
    {
        CurHealth = parameters.playerMaxHealth;
        weapDB = Resources.Load<WeaponsDataBase>("WeaponsDB/Weapons");
        weapDB.SetNamesAndLevels();
        AddWeapon("Test");
        AddWeapon("Test");
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

        var lookDir = PlayerCursor.GetDirection();

        if (lookDir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (lookDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
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
        var isActive = weapDB.IsActiveWeapon(wName);        
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
        var pref = weapDB.FindWeaponPrefab(wName, wLevel);
        var weap = Instantiate(pref, WeaponsOrigin);
        weap.Initialize(parameters);
        return weap;
    }

    private (bool isOnPlayer, int level) GetWeaponStatus(string wName)
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
        if(CurHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        CurHealth = Mathf.Clamp(CurHealth + heal, 0, parameters.playerMaxHealth);
    }
}