using UnityEngine;

public class BaseTrigger : MonoBehaviour
{

    [SerializeField] private Animator m_animator;
    [SerializeField] private Collider2D m_collider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_collider.enabled = false;
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