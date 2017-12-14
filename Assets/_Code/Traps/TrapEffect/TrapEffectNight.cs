using UnityEngine;

[CreateAssetMenu(menuName="TrapEffect/Night")]
public class TrapEffectNight : TrapEffect
{
    [SerializeField] private string m_inNightStateName;
        
    public override void ApplyOn(GameObject player)
    {
        player.GetComponent<Animator>().Play(m_inNightStateName);
        // TODO add 
    }
}