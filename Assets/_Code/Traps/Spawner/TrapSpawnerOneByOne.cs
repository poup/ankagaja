using System.Collections;
using Assets._Code;
using Assets._Code.Movement;
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
    
    [Space(10)]
    [SerializeField] protected bool m_hasDirection;
    [SerializeField] protected Vector2 m_direction;
    
    [Space(10)]
    [SerializeField] protected TrapActions.TrapActionsType m_actionOnTrigger;
    


    protected override IEnumerator Spawn()
    {
        for (int wave = 0; wave < m_waveCount; ++wave)
        {
            var count = Randomizer.Next(m_spawnNumberMax - m_spawnNumberMin) + m_spawnNumberMin;
            for (int i = 0; i < count; ++i)
            {
                var obj = SpawnOne();
                ApplyDirection(obj);
                obj.OnTriggerEnter += ActivateKillingBox;
                var timeBetweenSpawn = Randomizer.Next(m_timeBetweenSpawnMin, m_timeBetweenSpawnMax);
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            
            yield return new WaitForSeconds(m_timeBetweenWave);
        }
    }

    private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
    {
        TrapActions.DoAction(this, m_actionOnTrigger,triggered, triggerer );
    }


    private void OnValidate()
    {
        if (m_waveCount < 1)
            m_waveCount = 1;
        
        if (m_spawnNumberMin > m_spawnNumberMax)
            m_spawnNumberMax = m_spawnNumberMin;
    }
    
    private void ApplyDirection(TriggerBox obj)
    {

        if (m_hasDirection )
        {
            var direction = obj.GetComponent<Directionable>();
            if (direction)
            {
                direction.Direction = m_direction;
            }
            
        }
    }
}
