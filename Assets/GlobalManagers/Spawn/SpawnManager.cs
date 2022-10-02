using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnConfigBase> spawns;    

    private void Start()
    {        
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
