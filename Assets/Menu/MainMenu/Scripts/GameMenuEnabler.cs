using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuEnabler : MonoBehaviour
{
    Canvas canv;
    void Start()
    {
        canv = GetComponent<Canvas>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            canv.enabled = !canv.enabled;
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
}
