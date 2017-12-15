using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Code;
using UnityEngine;

public class TrapGlobalNight : TrapSpawner
{
    [SerializeField] protected float m_waitFullBlack = 0.8f;

    [SerializeField] protected float m_fadeInDuration = 0.3f;
    [SerializeField] protected float m_fadeOutDuration = 0.3f;

    [SerializeField] protected float m_duration = 8.0f;

    [SerializeField] private SpriteRenderer m_nightFilter;
    [SerializeField] private int m_iaCount;
    
    [Space(10)]
    [SerializeField] protected TrapActions.TrapActionsType m_actionOnTrigger;


    protected virtual IEnumerator Start()
    {
        var players = FindObjectsOfType<PlayerController>();
        yield return new WaitForSeconds(m_beforeSpawnDuration);

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

    protected override IEnumerator Spawn()
    {
        throw new NotImplementedException();
    }

    private List<GameObject> AddIA()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < m_iaCount; ++i)
        {
            var obj = SpawnOne();
            obj.OnTriggerEnter += ActivateKillingBox;
            list.Add(obj.gameObject);
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

            color.a = Mathf.Lerp(from, to, time / duration);
            m_nightFilter.color = color;
        }
    }

    private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
    {
        TrapActions.DoAction(this, m_actionOnTrigger, triggered, triggerer);
    }
}