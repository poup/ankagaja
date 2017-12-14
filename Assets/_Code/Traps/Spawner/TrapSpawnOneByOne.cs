using System.Collections;
using UnityEngine;

public class TrapSpawnOneByOne : BaseTrap
{
    [SerializeField] protected float m_timeBetweenSpawn;
    [SerializeField] protected int m_spawnNumberMin;
    [SerializeField] protected int m_spawnNumberMax;

    protected override IEnumerator Spawn()
    {
        var count = Randomizer.Next(m_spawnNumberMax - m_spawnNumberMin) + m_spawnNumberMin;
        for (int i = 0; i < count; ++i)
        {
            SpawnOne();
            yield return new WaitForSeconds(m_timeBetweenSpawn);
        }
    }


    private void OnValidate()
    {
        if (m_spawnNumberMin > m_spawnNumberMax)
            m_spawnNumberMax = m_spawnNumberMin;
    }
}
