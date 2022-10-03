using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessToggler : MonoBehaviour
{
    PostProcessLayer pp;
    void Start()
    {
        pp = GetComponent<PostProcessLayer>();
        pp.enabled = OptionsMenu.postProcessIsOn;
        OptionsMenu.OnPostProcToggle = OnTogglePP;
    }

    void OnDestroy()
    {
        OptionsMenu.OnPostProcToggle -= OnTogglePP;
    }

    void OnTogglePP()
    {
        pp.enabled = OptionsMenu.postProcessIsOn;
    }
}
