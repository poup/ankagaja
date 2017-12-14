using Assets.Scripts.PlayerManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D m_rigidBody;
	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_speedMax = 6.0f;
	
	[SerializeField] private float m_dashDuration = 2.0f;
	private float m_dashTime = 0.0f;
	
	private float m_dirX; 
	private float m_dirY;
	
	public PlayerInput Input { get; set; }
	
	void Update ()
	{
		var x = Input.H1();
		var y = Input.V1();
		
		var jump = Input.A();
		var dash = Input.X();
		
		
		var isMoving = Mathf.Abs(x) > 0.01f && Mathf.Abs(y) > 0.01f;
		var isJumping = false; // TODO
		var isDashing = false; // TODO
		
		if (isMoving)
		{
			m_dirX = x;
			m_dirY = y;
		}
		
		m_rigidBody.velocity = new Vector3(x, y, 0) * m_speedMax;
		
		m_animator.SetFloat("moveX", x);
		m_animator.SetFloat("moveY", y);
		m_animator.SetFloat("dirX", x);
		m_animator.SetFloat("dirY", y);
		
		m_animator.SetBool("isMoving", isMoving);
		m_animator.SetBool("isJumping", isJumping);
		m_animator.SetBool("isDashing", isDashing);
		
		
	}
}
