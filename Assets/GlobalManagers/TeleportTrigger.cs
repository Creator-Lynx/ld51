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

            var xOffset = other.transform.position.x < 0 ? 0.1f : -0.1f;
            var zOffset = other.transform.position.z < 0 ? 0.1f : -0.1f;
            var tpoffset = new Vector3(xOffset, 0, zOffset);

            other.transform.position += tpoffset;

            var enem = FindObjectsOfType<Enemy>();
            foreach (var e in enem)
            {
                var offset = e.transform.position - playerOldPos;
                e.transform.position = other.transform.position + offset;
            }

            var prj = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (var p in prj)
            {
                var offset = p.transform.position - playerOldPos;
                p.transform.position = other.transform.position + offset;
            }

            var meat = GameObject.FindGameObjectsWithTag("Meat");
            foreach (var m in meat)
            {
                var offset = m.transform.position - playerOldPos;
                m.transform.position = other.transform.position + offset;
            }
        }
    }
}
