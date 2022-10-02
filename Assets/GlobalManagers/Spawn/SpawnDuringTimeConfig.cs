using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Spawn/During", fileName = "During Config")]
public class SpawnDuringTimeConfig : SpawnSingleConfig
{
    public float TimeSeconds = 60f;
    public int SpawnCount = 10;
    public bool WaitForEnd = false;

    private float _spawnInterval;

    public override IEnumerator StartSpawnEvent(SpawnManager manager)
    {        
        _spawnInterval = TimeSeconds / SpawnCount;

        if (WaitForEnd)
        {
            yield return manager.StartCoroutine(SpawnProcess(manager));
        }
        else
        {
            manager.StartCoroutine(SpawnProcess(manager));
        }

        yield break;
    }

    private IEnumerator SpawnProcess(SpawnManager manager)
    {
        var count = 0;
        while (count < SpawnCount)
        {
            yield return manager.StartCoroutine(Spawn());
            yield return new WaitForSeconds(_spawnInterval);
            count++;
        }
        yield break;
    }
}
