using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public WeaponsDataBase weaponDB;
    public PlayerController player;
    private GameManager _gm;

    public Text ScoreText;

    public Text Timer;

    [Header("Active Weapon")]    
    public GameObject ActiveWeaponPanel;
    public GameObject ActiveWeaponReady;
    public Image ActiveWeaponIcon;
    public Text ActiveWeaponLevel;

    [Header("Passive Weapon 1")]
    public GameObject PassiveWeapon1Panel;    
    public Image PassiveWeapon1Icon;
    public Text PassiveWeapon1Level;

    [Header("Passive Weapon 2")]
    public GameObject PassiveWeapon2Panel;
    public Image PassiveWeapon2Icon;
    public Text PassiveWeapon2Level;

    [Header("Passive Weapon 3")]
    public GameObject PassiveWeapon3Panel;
    public Image PassiveWeapon3Icon;
    public Text PassiveWeapon3Level;

    [Header("Health")]
    public Slider HealthSlider;
    public Text HealthText;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(_gm.isTimer)
        {
            Timer.gameObject.SetActive(true);
            Timer.text = ((int)_gm.timer).ToString();
        }
        else
        {
            Timer.gameObject.SetActive(false);
        }

        ScoreText.text = $"Score: {_gm.Score} Kills: {_gm.KillCount}";
        if (player)
        {
            gameObject.SetActive(true);
            HealthSlider.maxValue = player.parameters.playerMaxHealth;
            HealthSlider.value = player.CurHealth;
            HealthText.text = $"{player.CurHealth}/{player.parameters.playerMaxHealth}";

            if (player.ActiveWeapon)
            {
                ActiveWeaponPanel.SetActive(true);
                ActiveWeaponReady.SetActive(!player.ActiveWeapon.Ready);
                ActiveWeaponIcon.sprite = weaponDB.GetWeaponIcon(player.ActiveWeapon.WeaponName);
                ActiveWeaponLevel.text = player.ActiveWeapon.WeaponLevel.ToString();
            }
            else
            {
                ActiveWeaponPanel.SetActive(false);
            }

            var passWeapCount = player.PassiveWeapons.Count;
            if (passWeapCount > 0)
            {
                PassiveWeapon1Panel.SetActive(true);
                PassiveWeapon1Icon.sprite = weaponDB.GetWeaponIcon(player.PassiveWeapons[0].WeaponName);
                PassiveWeapon1Level.text = player.PassiveWeapons[0].WeaponLevel.ToString();
            }
            else
            {
                PassiveWeapon1Panel.SetActive(false);
            }

            if (passWeapCount > 1)
            {
                PassiveWeapon2Panel.SetActive(true);
                PassiveWeapon2Icon.sprite = weaponDB.GetWeaponIcon(player.PassiveWeapons[1].WeaponName);
                PassiveWeapon2Level.text = player.PassiveWeapons[1].WeaponLevel.ToString();
            }
            else
            {
                PassiveWeapon2Panel.SetActive(false);
            }

            if (passWeapCount > 2)
            {
                PassiveWeapon3Panel.SetActive(true);
                PassiveWeapon3Icon.sprite = weaponDB.GetWeaponIcon(player.PassiveWeapons[2].WeaponName);
                PassiveWeapon3Level.text = player.PassiveWeapons[2].WeaponLevel.ToString();
            }
            else
            {
                PassiveWeapon3Panel.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
