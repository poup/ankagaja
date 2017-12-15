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
        foreach (var p in players)
        {
            p.PlayAnimState(m_inNightStateName);
        }
        
    }
}
