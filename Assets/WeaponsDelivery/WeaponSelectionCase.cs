using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionCase : MonoBehaviour
{
    public Image WeaponIcon;
    public Text WeaponName;
    public Text WeaponDesc;

    private GameManager _gm;
    private WeaponSelectionDialog _dialog;
    private string _wName;

    private void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    public void Initialize(WeaponSelectionDialog dialog, string wName)
    {
        _dialog = dialog;
        _wName = wName;

        var player = FindObjectOfType<PlayerController>();
        var status = player.GetWeaponStatus(wName);

        WeaponIcon.sprite = _gm.weapDB.GetWeaponIcon(wName);

        string displayName = "";
        if(status.isOnPlayer)
        {
            displayName = $"<b>{wName}</b> (Level {status.level + 1})";
            WeaponDesc.text = _gm.weapDB.GetDescription(wName, status.level + 1);
        }
        else
        {
            displayName = $"<b>{wName}</b> (Level 1)";
            WeaponDesc.text = _gm.weapDB.GetDescription(wName, 1);
        }
        WeaponName.text = displayName;

        
    }

    public void OnClick()
    {
        _dialog.Select(_wName);
    }
}
