using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawnOneByOne : BaseTrap
{
    [SerializeField] protected float m_timeBetweenSpawn;
    [SerializeField] protected int m_spawnNumber;

    protected override IEnumerator Spawn()
    {
        for (int i = 0; i < m_spawnNumber; ++i)
        {
            SpawnOne();
            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }
    }


}
