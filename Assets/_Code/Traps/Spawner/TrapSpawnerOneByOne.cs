using System.Collections;
using UnityEngine;

public class TrapSpawnerOneByOne : TrapSpawner
{
    [SerializeField] protected int m_waveCount = 1;
    [SerializeField] protected float m_timeBetweenWave;
    
    [Space(10)]
    [SerializeField] protected float m_timeBetweenSpawnMin;
    [SerializeField] protected float m_timeBetweenSpawnMax;
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
                var timeBetweenSpawn = Randomizer.Next(m_timeBetweenSpawnMin, m_timeBetweenSpawnMax);
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            
            yield return new WaitForSeconds(m_timeBetweenWave);
        }
    }

    private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
    {
        var player = triggerer.GetComponent<PlayerController>();
        if (player != null)
        {
            player.gameObject.SetActive(false);
            StartCoroutine(WIP_Reactivate(player.gameObject));
            
            player.PlayAnimState(m_deadStateName);
        }
    }

    private IEnumerator WIP_Reactivate(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(true);
    }


    private void OnValidate()
    {
        if (m_waveCount < 1)
            m_waveCount = 1;
        
        if (m_spawnNumberMin > m_spawnNumberMax)
            m_spawnNumberMax = m_spawnNumberMin;
    }
}
