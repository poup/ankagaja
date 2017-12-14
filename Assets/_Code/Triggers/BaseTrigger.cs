using UnityEngine;

public class BaseTrigger : MonoBehaviour
{

    [SerializeField] private Animator m_animator;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!enabled)
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            enabled = false;
            var player = other.gameObject;
            ChangeAnimPlayer(player);
            ChangeAnimTrigger();
            ActivateTrap();
        }
    }

    private void ActivateTrap()
    {
        TrapManager.Instance.StartTrap();
    }

    protected virtual void ChangeAnimPlayer(GameObject player)
    {
        Debug.Log(player.name + ": anim active trigger");
    }

    protected virtual void ChangeAnimTrigger()
    {
        Debug.Log("active trigger " + gameObject.name);
    }
}