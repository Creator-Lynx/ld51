using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public bool horizontal = false;

    private void OnTriggerEnter(Collider other)
    {
        var hor = horizontal ? -1 : 1;
        var vert = horizontal ? 1 : -1;
        if (other.tag == "Player")
        {
            var playerOldPos = other.transform.position;
            other.transform.position 
                = new Vector3(other.transform.position.x * hor, other.transform.position.y, other.transform.position.z * vert);

            var enem = FindObjectsOfType<Enemy>();
            foreach(var e in enem)
            {
                var offset = e.transform.position - playerOldPos;
                e.transform.position = other.transform.position + offset;
            }
        }
    }
}
