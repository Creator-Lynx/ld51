using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnConfigBase : ScriptableObject
{
    [Header("Borders")]
    private float upBorder = 0;
    private float downBorder = 0;

    private float leftBorder = 0;
    private float rightBorder = 0;

    [Header("Directions")]
    public VertPos VertSpawnPos = VertPos.Any;
    public HorPos HorSpawnPos = HorPos.Any;

    public abstract IEnumerator StartSpawnEvent(SpawnManager manager);

    protected void CalculateBorders()
    {
        var cam = Camera.main;
        var ray1 = cam.ScreenPointToRay(Vector3.zero);
        if (Physics.Raycast(ray1, out var hit1, 100))
        {
            downBorder = hit1.point.z - 1;
            leftBorder = hit1.point.x - 1;
        }

        var ray2 = cam.ScreenPointToRay(new Vector3(Screen.width, Screen.height));
        if (Physics.Raycast(ray2, out var hit2, 100))
        {
            upBorder = hit2.point.z + 1;
            rightBorder = hit2.point.x + 1;
        }
    }

    protected Vector3 CalculateSpawnPoint()
    {
        var hor = Random.Range(leftBorder, rightBorder);
        var vert = Random.Range(downBorder, upBorder);

        if(VertSpawnPos != VertPos.Any)
        {
            vert = VertSpawnPos == VertPos.Up ? upBorder : downBorder;
        }

        if(HorSpawnPos != HorPos.Any)
        {
            hor = HorSpawnPos == HorPos.Left ? leftBorder : rightBorder;
        }

        if (HorSpawnPos == HorPos.Any && VertSpawnPos == VertPos.Any)
        {
            var rnd = Random.Range(-10, 10);
            if (rnd > 0)
            {
                hor = PullUpToBorder(leftBorder, rightBorder);
            }
            else
            {
                vert = PullUpToBorder(downBorder, upBorder);
            }
        }

        return new Vector3(hor, 0, vert);
    }

    private float PullUpToBorder(float minBorder, float maxBorder)
    {
        var rnd = Random.Range(-10, 10);
        return rnd > 0 ? maxBorder : minBorder;
    }
}

public enum VertPos
{
    Any,
    Up,
    Down
}

public enum HorPos
{
    Any,
    Left,
    Right
}
