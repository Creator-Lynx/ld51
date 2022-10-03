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

    [Header("Active Weapon")]    
    public GameObject ActiveWeaponPanel;
    public GameObject ActiveWeaponReady;
    public Image ActiveWeaponIcon;
    public Text ActiveWeaponLevel;

    [Header("Health")]
    public Slider HealthSlider;
    public Text HealthText;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
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
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
