using System.Collections;
using System.Linq;
using UnityEngine;

public class TrapGlobalNight : BaseTrap
{
    [SerializeField] protected float m_durationBeforeActivation;
    [SerializeField] private string m_inNightStateName;

  
    protected virtual IEnumerator Start()
    {
        var players = FindObjectsOfType<PlayerController>();
        yield return new WaitForSeconds(m_durationBeforeActivation);
        // TODO gros sprite noir pour tout masquer 
        foreach (var p in players)
        {
            // TODO Ajouter des yeux sur les persos , dans un layer au dessus du gros sprtie noirs ?
            p.PlayAnimState(m_inNightStateName);
        }
        
    }
}
