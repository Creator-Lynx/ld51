using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestrictions : MonoBehaviour
{
    [Header("Player")]
    public Transform Player;

    public float PlayerXMin;
    public float PlayerXMax;

    public float PlayerZMin;
    public float PlayerZMax;

    [Header("Camera")]
    public Transform Camera;

    public float CamXMin;
    public float CamXMax;

    public float CamZMin;
    public float CamZMax;    

    private void LateUpdate()
    {
        var posX = Mathf.Clamp(Player.position.x, PlayerXMin, PlayerXMax);
        var posZ = Mathf.Clamp(Player.position.z, PlayerZMin, PlayerZMax);
        Player.position = new Vector3(posX, 0, posZ);

        var posXCam = Mathf.Clamp(Camera.position.x, CamXMin, CamXMax);
        var posZCam = Mathf.Clamp(Camera.position.z, CamZMin, CamZMax);
        Camera.position = new Vector3(posXCam, Camera.position.y, posZCam);
    }

    private void OnDrawGizmos()
    {
        DrawRestrictions(PlayerXMin, PlayerXMax, PlayerZMin, PlayerZMax);
        Gizmos.color = Color.yellow;
        DrawRestrictions(CamXMin, CamXMax, CamZMin, CamZMax);
    }

    private void DrawRestrictions(float minX, float maxX, float minZ, float maxZ)
    {
        var leftDown = new Vector3(minX, 0, minZ);
        var leftUp = new Vector3(minX, 0, maxZ);

        var rightDown = new Vector3(maxX, 0, minZ);
        var rightUp = new Vector3(maxX, 0, maxZ);

        Gizmos.DrawLine(rightUp, rightDown);
        Gizmos.DrawLine(rightDown, leftDown);
        Gizmos.DrawLine(leftDown, leftUp);
        Gizmos.DrawLine(leftUp, rightUp);
    }
}
