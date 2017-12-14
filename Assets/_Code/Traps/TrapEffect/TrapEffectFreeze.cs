using UnityEngine;

[CreateAssetMenu(menuName="TrapEffect/Freeze")]
public class TrapEffectFreeze : TrapEffect
{
    [SerializeField] private float m_slipFactor;
    [SerializeField] private float m_waitBeforeFreeze;
    [SerializeField] private float m_hitCountToUnfreeze;
    
    [SerializeField] private string m_playerAnim;
        
        
    public override void ApplyOn(GameObject player)
    {
        // TODO
    }
}