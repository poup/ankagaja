using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour
{
    [SerializeField] protected float m_beforeSpawnDuration;
    [SerializeField] protected float m_timeBetweenSpawn;
    [SerializeField] protected int m_spawnNumber;
    [SerializeField] protected KillingBox m_killingBoxPrefab;
    [SerializeField] protected TrapSpawnZone m_spawnZone;

  
    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_beforeSpawnDuration);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < m_spawnNumber; ++i)
        {
            SpawnOne();
            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }
    }

    private void SpawnOne()
    {
        var position = m_spawnZone.GetPosition();
        Instantiate(m_killingBoxPrefab, position, Quaternion.identity);
    }
}
