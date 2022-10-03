using UnityEngine;
using UnityEngine.UI;

public class VersionSetter : MonoBehaviour
{

    void Start()
    {
        GetComponent<Text>().text = 'v' + Application.version;
    }


}
