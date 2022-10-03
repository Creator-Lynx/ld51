using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContentOpen : MonoBehaviour
{
    public DescriptionPanel descriptionPanel;
    public int ContentID = 0;

    public void ShowContent()
    {
        descriptionPanel.OpenPanel(ContentID);
    }
}
