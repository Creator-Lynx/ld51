using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Spawn/Wait", fileName = "Wait Config")]
public class WaitSpawnConfig : SpawnConfigBase
{
    public float WaitSeconds = 1f;

    public override IEnumerator StartSpawnEvent(SpawnManager manager)
    {        
        yield return new WaitForSeconds(WaitSeconds);
    }
}
