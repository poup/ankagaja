using Assets.Scripts.PlayerManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Transform m_characterRenderTransform;
	[SerializeField] private Rigidbody2D m_rigidBody;
	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_speedMax = 12.0f;
	[SerializeField] private float m_acceleration = 600.0f;

	[SerializeField] private float m_playerContactFactor = 1.0f;
	
	[SerializeField] private float m_dashDuration = 2.0f;
	[SerializeField] private float m_dashSpeedBonus = 0.0f;
	[SerializeField] private float m_dashWeightBonus = 0.0f;
	private float m_dashTime = -1.0f;
	
	
	[SerializeField] private float m_jumpDuration = 1.0f;
	[SerializeField] private AnimationCurve m_jumpCurve = new AnimationCurve();
	private float m_jumpTime = -1.0f;

	private float m_moveX;
	private float m_moveY;
	private float m_dirX;
	private float m_dirY;

	public PlayerInput Input { get; set; }
	public Player Player { get; set; }


	private ContactPoint2D m_contact;
	
	public bool IsJumping { get { return m_jumpTime >= 0;  } }
	public bool IsDashing { get { return m_dashTime >= 0;  } }

	public float JumpingHeight
	{
		get
		{
			if (!IsJumping)
				return 0;
			return m_jumpCurve.Evaluate(m_jumpTime / m_jumpDuration);
		}
	}


	void Update()
	{
#if TOTO
		var x = Input.H1();
		var y = Input.V1();

		var jump = Input.ADown();
		var dash = Input.XDown();
#else
		var x = UnityEngine.Input.GetAxis("Horizontal") ;
		var y = UnityEngine.Input.GetAxis("Vertical") ;


		var jump = UnityEngine.Input.GetButtonDown("Jump") ;
		var dash = UnityEngine.Input.GetButtonDown("Fire1") ;
#endif

		var dt = Time.deltaTime;
		DashingUpdate(dash, dt);
		JumpingUpdate(jump, dt);

		var isMoving = Mathf.Abs(x) > 0.01f || Mathf.Abs(y) > 0.01f || IsDashing;

		if (isMoving)
		{
			m_dirX = x;
			m_dirY = y;
		}


//		var isMoving = Mathf.Abs(x) > 0.001f || Mathf.Abs(y) > 0.001f;
//		var isJumping = false; // TODO
//		var isDashing = false; // TODO
//
//		if (Mathf.Abs(x) > 0.001f)
//		{
//			m_dirX = x;
//		}
//		if (Mathf.Abs(y) > 0.001f)
//		{
//			m_dirY = y;
//		}

		if (!IsDashing)
		{
			m_moveX = x;
			m_moveY = y;
		}

		m_animator.SetFloat("moveX", x);
		m_animator.SetFloat("moveY", y);
		m_animator.SetFloat("dirX", m_dirX);
		m_animator.SetFloat("dirY", m_dirY);

		m_animator.SetBool("isMoving", isMoving);
		m_animator.SetBool("isJumping", IsJumping);
		m_animator.SetBool("isDashing", IsDashing);
	}

	private void DashingUpdate(bool dash, float dt)
	{
		var dashing = IsDashing;
		if (!dashing && dash)
		{
			m_dashTime = 0.0f;
			dashing = true;
		}
		
		if (dashing)
		{
			m_dashTime += dt;
		}
		if (m_dashTime > m_dashDuration)
		{
			m_dashTime = -1;
		}
	}

	private void JumpingUpdate(bool jump, float dt)
	{
		if (IsDashing)
			return;
		
		var jumping = IsJumping;
		if (!jumping && jump)
		{
			m_jumpTime = 0.0f;
			jumping = true;
		}
		
		if (jumping)
		{
			m_jumpTime += dt; 
			if (m_jumpTime > m_jumpDuration)
			{
				m_jumpTime = -1;
			}
		}

		var p = m_characterRenderTransform.localPosition;
		m_characterRenderTransform.localPosition = new Vector3(p.x, JumpingHeight, p.z);
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

	public void OnEffect(HitEffect hitEffect)
	{
		Debug.Log(name + " receive effect " + hitEffect);
	}
}