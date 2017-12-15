using System;
using System.Collections;
using UnityEngine;

public abstract class TrapSpawner : BaseTrap
{
    // TODO ajouter sons 
    
    [SerializeField] protected float m_beforeSpawnDuration;
    [SerializeField] protected TriggerBox[] m_killingBoxPrefab;
    protected TrapSpawnZone[] m_spawnZones;


    protected virtual void Awake()
    {
        m_spawnZones = GetComponentsInChildren<TrapSpawnZone>();
        if (m_spawnZones == null || m_spawnZones.Length == 0)
            throw new Exception("faut mettre au moins une spawnZone");
    }

  
    protected virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(m_beforeSpawnDuration);
        StartCoroutine(Spawn());
    }
    protected abstract IEnumerator Spawn();
    
    protected TriggerBox SpawnOne()
    {
        var spawnZone = m_spawnZones.PickRandom();
        var killingBoxPrefab = m_killingBoxPrefab.PickRandom();
        var position = spawnZone.GetPosition();
        
        return Instantiate(killingBoxPrefab, position, Quaternion.identity, transform);
    }
}
