using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnConfigBase> spawns;
    public Enemy FirstEnemy;
    

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);

        var cam = Camera.main;
        var ray1 = cam.ScreenPointToRay(new Vector3(0, Screen.height));
        if (Physics.Raycast(ray1, out var hit, 100)) { }

        var offset = new Vector3(1, 0, 1);
        var en = Instantiate(FirstEnemy, hit.point + offset, Quaternion.identity);

        en.OnCorrupted += StartActivePhase;
    }

    private void StartActivePhase()
    {        
        FindObjectOfType<GameManager>()?.StartActivePhase();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        foreach (var spawn in spawns)
        {
            yield return StartCoroutine(spawn.StartSpawnEvent(this));
        }
    }
}
