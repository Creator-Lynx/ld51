using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedStateAwait : MonoBehaviour
{
    public Material CorruptedMat;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameManager>().OnActivePhase += SetCorruptedState;
    }

    private void SetCorruptedState()
    {
        Debug.Log("Corrupted ground");
        GetComponent<MeshRenderer>().material = CorruptedMat;
    }
}
