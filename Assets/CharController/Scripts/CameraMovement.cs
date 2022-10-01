using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;    
    
    void Update()
    {
        if (Player)
        {
            var newPos = new Vector3(Player.position.x, 10, Player.position.z - 9);
            transform.position = Vector3.Lerp(transform.position, newPos, 0.01f);
        }
    }
}
