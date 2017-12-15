using System.Collections;
using UnityEngine;

public class TrapSpawnerOneByOne : TrapSpawner
{
    [SerializeField] protected int m_waveCount = 1;
    [SerializeField] protected float m_timeBetweenWave;
    
    [Space(10)]
    [SerializeField] protected float m_timeBetweenSpawn;
    [SerializeField] protected int m_spawnNumberMin = 10;
    [SerializeField] protected int m_spawnNumberMax = 20;
    
    [SerializeField] private string m_deadStateName;

    protected override IEnumerator Spawn()
    {
        for (int wave = 0; wave < m_waveCount; ++wave)
        {
            var count = Randomizer.Next(m_spawnNumberMax - m_spawnNumberMin) + m_spawnNumberMin;
            for (int i = 0; i < count; ++i)
            {
                var obj = SpawnOne();
                obj.OnTriggerEnter += ActivateKillingBox;
                yield return new WaitForSeconds(m_timeBetweenSpawn);
            }
            
            yield return new WaitForSeconds(m_timeBetweenWave);
        }
    }

    private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
    {
        var player = triggerer.GetComponent<PlayerController>();
        if (player != null)
        {
            player.PlayAnimState(m_deadStateName);
        }
    }


    private void OnValidate()
    {
        if (m_waveCount < 1)
            m_waveCount = 1;
        
        if (m_spawnNumberMin > m_spawnNumberMax)
            m_spawnNumberMax = m_spawnNumberMin;
    }
}
