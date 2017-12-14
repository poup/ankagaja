using Assets.Scripts.PlayerManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D m_rigidBody;
	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_speedMax = 12.0f;
	[SerializeField] private float m_acceleration = 600.0f;

	[SerializeField] private float m_playerContactFactor = 1.0f;
	[SerializeField] private float m_dashDuration = 2.0f;
	private float m_dashTime = 0.0f;

	private float m_moveX;
	private float m_moveY;
	private float m_dirX;
	private float m_dirY;

	public PlayerInput Input { get; set; }
	public Player Player { get; set; }


	private ContactPoint2D m_contact;

	private void OnDrawGizmos()
	{
		var dir = m_contact.normal * m_contact.normalImpulse * m_playerContactFactor;
		if (dir.magnitude > 0)
		{
			var pos = transform.position;
			Gizmos.DrawLine(pos, pos + new Vector3(dir.x, dir.y, 0));
		}
	}

	void Update()
	{
#if! TOTO
		var x = Input.H1();
		var y = Input.V1();

		var jump = Input.A();
		var dash = Input.X();
#else
		var x = UnityEngine.Input.GetAxis("Horizontal") ;
		var y = UnityEngine.Input.GetAxis("Vertical") ;


		var jump = false;//Input.A();
		var dash = false; // Input.X();
		#endif


		var isMoving = Mathf.Abs(x) > 0.01f || Mathf.Abs(y) > 0.01f;
		var isJumping = false; // TODO
		var isDashing = false; // TODO

		if (isMoving)
		{
			m_dirX = x;
			m_dirY = y;
		}

		m_moveX = x;
		m_moveY = y;

		m_animator.SetFloat("moveX", x);
		m_animator.SetFloat("moveY", y);
		m_animator.SetFloat("dirX", x);
		m_animator.SetFloat("dirY", y);

		m_animator.SetBool("isMoving", isMoving);
		m_animator.SetBool("isJumping", isJumping);
		m_animator.SetBool("isDashing", isDashing);
	}

	private void FixedUpdate()
	{
		var move = new Vector2(m_moveX, m_moveY) * m_acceleration;
		m_rigidBody.AddForce(move);

		var speed = m_rigidBody.velocity.magnitude;
		if (speed > m_speedMax)
		{
			m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_speedMax;
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject)
		{
			if (other.contacts.Length > 0)
			{
				m_contact = other.contacts[0];
				var contactFactor = m_playerContactFactor;
				m_rigidBody.AddForce(m_contact.normal * m_contact.normalImpulse * contactFactor, ForceMode2D.Impulse);
			}
		}
	}

	public void AddReward(int value)
	{
		Player.AddReward(value);
	}
}