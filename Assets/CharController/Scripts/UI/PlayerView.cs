using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public WeaponsDataBase weaponDB;
    public PlayerController player;

    [Header("Active Weapon")]    
    public GameObject ActiveWeaponPanel;
    public GameObject ActiveWeaponReady;
    public Image ActiveWeaponIcon;
    public Text ActiveWeaponLevel;

    [Header("Health")]
    public Slider HealthSlider;
    public Text HealthText;

    private void Update()
    {
        if (player)
        {
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
        }
    }
}