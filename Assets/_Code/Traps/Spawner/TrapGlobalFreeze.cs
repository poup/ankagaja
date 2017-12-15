using System.Collections;
using UnityEngine;

public class TrapGlobalFreeze : BaseTrap
{
    [SerializeField] protected float m_durationBeforeActivation;
    [SerializeField] private float m_slipFactor;
    [SerializeField] private float m_waitBeforeFreeze;
    [SerializeField] private float m_hitCountToUnfreeze;
    
    [SerializeField] private string m_playerAnim;

    protected virtual void Awake()
    {
    }
  
    protected virtual IEnumerator Start()
    {
        var players = FindObjectsOfType<PlayerController>();
        yield return new WaitForSeconds(m_durationBeforeActivation);
        
        
    }
    
    
}
