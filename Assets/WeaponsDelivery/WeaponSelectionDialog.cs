using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSelectionDialog : MonoBehaviour
{
    public List<string> WeaponsToSelect;
    public WeaponSelectionCase CasePref;
    public Transform CaseOrigin;

    private List<GameObject> created = new List<GameObject>();

    public void ShowDialog()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var weaps = WeaponsToSelect.OrderBy(x => Random.Range(-100, 100)).Take(3);
        foreach(var w in weaps)
        {
            var c = Instantiate(CasePref, CaseOrigin);
            c.Initialize(this, w);
            created.Add(c.gameObject);
        }
    }

    public void Select(string wName)
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);

        FindObjectOfType<PlayerController>().AddWeapon(wName);

        foreach(var cr in created)
        {
            Destroy(cr);
        }
        created.Clear();
    }
}
