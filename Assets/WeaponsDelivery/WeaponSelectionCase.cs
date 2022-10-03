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

    private bool interactible = false;

    public void Initialize(WeaponSelectionDialog dialog, string wName)
    {
        _gm = FindObjectOfType<GameManager>();
        _dialog = dialog;
        _wName = wName;

        var player = FindObjectOfType<PlayerController>();
        var status = player.GetWeaponStatus(wName);

        if (status.level == _gm.weapDB.GetMaxLevel(wName))
        {
            gameObject.SetActive(false);
            return;
        }

        WeaponIcon.sprite = _gm.weapDB.GetWeaponIcon(wName);

        string displayName = "";
        string isActive = _gm.weapDB.IsActiveWeapon(wName) ? " [active]" : " [passive]";
        if(status.isOnPlayer)
        {
            displayName = $"<b>{wName}</b> (Level {status.level + 1})" + isActive;
            WeaponDesc.text = _gm.weapDB.GetDescription(wName, status.level + 1);
        }
        else
        {
            displayName = $"<b>{wName}</b> (Level 1)" + isActive;
            WeaponDesc.text = _gm.weapDB.GetDescription(wName, 1);
        }
        WeaponName.text = displayName;

        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        interactible = true;
    }

    public void OnClick()
    {
        if (interactible)
        {
            _dialog.Select(_wName);
        }
    }
}
