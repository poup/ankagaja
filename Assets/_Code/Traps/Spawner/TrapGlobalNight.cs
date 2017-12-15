using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapGlobalNight : BaseTrap
{
    [SerializeField] protected float m_durationBeforeActivation = 0;
    [SerializeField] protected float m_waitFullBlack = 0.8f;
    
    [SerializeField] protected float m_fadeInDuration = 0.3f;
    [SerializeField] protected float m_fadeOutDuration = 0.3f;
    
    [SerializeField] protected float m_duration = 8.0f;
    
    [SerializeField] private string m_inNightStateName;
    [SerializeField] private SpriteRenderer m_nightFilter;
    [SerializeField] private GameObject m_iaPrefab;
    [SerializeField] private int m_iaCount;
    TrapSpawnZone[] m_spawnZones;
    


    protected virtual void Awake()
    {
        m_spawnZones = GetComponentsInChildren<TrapSpawnZone>();
        if (m_spawnZones == null || m_spawnZones.Length == 0)
            throw new Exception("faut mettre au moins une spawnZone");
    }

    

  
    protected virtual IEnumerator Start()
    {
        var players = FindObjectsOfType<PlayerController>();
        yield return new WaitForSeconds(m_durationBeforeActivation);

        yield return StartCoroutine(NightFade(0, 1, m_fadeInDuration));
        
        yield return new WaitForSeconds(m_waitFullBlack);
        foreach (var p in players)
        {
            p.ShowGrosYeux(true);
        }

        var iaAdded = AddIA();
        yield return new WaitForSeconds(m_duration);

        RemoveIA(iaAdded);
        yield return StartCoroutine(NightFade(1, 0, m_fadeOutDuration));
        foreach (var p in players)
        {
            p.ShowGrosYeux(false);
        }
        
    }


    private List<GameObject> AddIA()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < m_iaCount; ++i)
        {
            var pos = m_spawnZones.PickRandom().GetPosition();
            var go = Instantiate(m_iaPrefab, pos, Quaternion.identity, transform);
            list.Add(go);
        }

        return list;
    }
    
    private void RemoveIA(List<GameObject> list)
    {
        foreach (var go in list)
        {
            Destroy(go);
        }
    }

    private IEnumerator NightFade(float from, float to, float duration)
    {
        var frame = new WaitForEndOfFrame();
        float time = 0;
        var color = m_nightFilter.color;
        while (time < duration)
        {
            yield return frame;
            time += Time.deltaTime;

            color.a = Mathf.Lerp(from, to, time/duration);
            m_nightFilter.color = color;
        }
           
    }
}
