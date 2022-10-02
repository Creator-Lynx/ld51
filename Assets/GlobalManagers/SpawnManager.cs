using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Camera cam;

    [Header("Borders")]
    private float upBorder = 0;
    private float downBorder = 0;

    private float leftBorder = 0;
    private float rightBorder = 0;

    [Header("Prefabs")]
    public GameObject rabbit;

    private void Start()
    {
        CalculateBorders();
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        CalculateBorders();
    }

    private void CalculateBorders()
    {
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

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < 100; i++)
        {
            var pos = CalculateSpawnPoint();            
            var en = Instantiate(rabbit, pos, Quaternion.identity).GetComponent<Enemy>();
            en.Corrupt();
            yield return new WaitForSeconds(1f);
        }
    }

    private Vector3 CalculateSpawnPoint()
    {
        var hor = Random.Range(leftBorder, rightBorder);
        var vert = Random.Range(downBorder, upBorder);

        var rnd = Random.Range(-10, 10);
        if (rnd > 0)
        {
            hor = PullUpToBorder(leftBorder, rightBorder);
        }
        else
        {
            vert = PullUpToBorder(downBorder, upBorder);
        }        

        return new Vector3(hor, 0, vert);
    }

    private float PullUpToBorder(float minBorder, float maxBorder)
    {        
        var rnd = Random.Range(-10, 10);        
        return rnd > 0 ? maxBorder : minBorder;
    }

    //private void OnDrawGizmos()
    //{
    //    var lD = new Vector3(leftBorder, 0, downBorder);
    //    Gizmos.DrawSphere(lD, 1f);

    //    var rU = new Vector3(rightBorder, 0, upBorder);
    //    Gizmos.DrawSphere(rU, 1f);
    //}
}
