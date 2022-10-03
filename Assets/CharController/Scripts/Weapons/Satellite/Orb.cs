using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private Satellite_Lvl1 origin;    

    private void Start()
    {
        origin = GetComponentInParent<Satellite_Lvl1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        origin.OnTrigger(other);
    }
}
