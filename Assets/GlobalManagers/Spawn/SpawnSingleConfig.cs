using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Spawn/Single", fileName = "Single Config")]
public class SpawnSingleConfig : SpawnConfigBase
{
    public Enemy EnemyPrefab;

    public override IEnumerator StartSpawnEvent(SpawnManager manager)
    {        
        yield return manager.StartCoroutine(Spawn());
    }

    protected IEnumerator Spawn()
    {
        CalculateBorders();
        var pos = CalculateSpawnPoint();
        Instantiate(EnemyPrefab, pos, Quaternion.identity);
        yield break;
    }
}
