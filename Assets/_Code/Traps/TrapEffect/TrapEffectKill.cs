using UnityEngine;

    [CreateAssetMenu(menuName="TrapEffect/Kill")]
    public class TrapEffectKill : TrapEffect
    {
        [SerializeField] private string m_deadStateName;
        
        
        public override void ApplyOn(GameObject player)
        {
            player.GetComponent<Animator>().Play(m_deadStateName);
        }
    }