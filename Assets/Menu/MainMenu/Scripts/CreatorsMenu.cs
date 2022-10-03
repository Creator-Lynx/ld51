using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorsMenu : MonoBehaviour
{
    public void OpenURL(int id)
    {        
        switch(id)
        {
            case 0:
                Application.OpenURL("https://t.me/gamedevils");
                break;

            case 1:
                Application.OpenURL("https://t.me/GameTrainProject");
                break;

            case 2:
                Application.OpenURL("https://t.me/Frameblade_team");
                break;
        }
    }
}
