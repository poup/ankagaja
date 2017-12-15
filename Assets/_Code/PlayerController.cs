using Assets.Scripts.PlayerManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer m_eyesInNight;
	
	[SerializeField] private Transform m_characterRenderTransform;
	[SerializeField] private Rigidbody2D m_rigidBody;
	[SerializeField] private Animator m_animator;
	[SerializeField] private float m_speedMax = 12.0f;
	[SerializeField] private float m_acceleration = 600.0f;

	[SerializeField] private float m_playerContactFactor = 1.0f;
	
	[SerializeField] private float m_dashDuration = 2.0f;
	[SerializeField] private float m_dashSpeedBonus = 50.0f;
	[SerializeField] private float m_dashWeightBonus = 0.0f;
	[SerializeField] private float m_keepDashForce = 1.0f;
	
	private float m_dashTime = -1.0f;
	private Vector2 m_currentDashDirection;
	
	
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
	
	public bool IsDead { get; set; }

	private float m_currentMaxSpeed;

	public float JumpingHeight
	{
		get
		{
			if (!IsJumping)
				return 0;
			return m_jumpCurve.Evaluate(m_jumpTime / m_jumpDuration);
		}
	}

	void Start()
	{
		m_currentMaxSpeed = m_speedMax;
		IsDead = false;
	}


	void Update()
	{
#if !TOTO
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
		if (IsDashing)
		{
			m_dashTime -= dt;
			var speed = m_rigidBody.velocity.magnitude;
			if (speed <= m_speedMax * 1.1)
			{
				m_dashTime = -1;
				m_currentMaxSpeed = m_speedMax;
			}

			m_rigidBody.AddForce(m_currentDashDirection * dt * m_acceleration * m_keepDashForce,ForceMode2D.Force);
			
			return;
		}

		if (dash)
		{
			m_dashTime = 2.0f;

			m_currentMaxSpeed = m_speedMax + m_dashSpeedBonus;

			m_currentDashDirection = new Vector2(Input.H1(), Input.V1()).normalized;
			m_rigidBody.AddForce(m_currentDashDirection * dt * m_acceleration * 25, ForceMode2D.Impulse);
		}

//		var dashing = IsDashing;
//		if (!dashing && dash)
//		{
//			m_dashTime = 0.0f;
//			dashing = true;
//		}
//		
//		if (dashing)
//		{
//			m_dashTime += dt;
//		}
//		if (m_dashTime > m_dashDuration)
//		{
//			m_dashTime = -1;
//		}
	}
//
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
		_impactDelay -= Time.fixedDeltaTime;
		if (IsDashing)
			return;
		
		var move = new Vector2(m_moveX, m_moveY) * m_acceleration;
		m_rigidBody.AddForce(move);

		var speed = m_rigidBody.velocity.magnitude;
		if (speed > m_currentMaxSpeed && !IsDashing)
		{
			m_rigidBody.velocity = m_rigidBody.velocity.normalized * m_currentMaxSpeed;
		}
	}

	private float _impactDelay = -1;
	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player") && other.gameObject != gameObject && _impactDelay<=0)
		{
			if (other.contacts.Length > 0)
			{
				m_dashTime = -1;
				m_contact = other.contacts[0];
				var contactFactor = m_playerContactFactor;
				m_rigidBody.AddForce(m_contact.normal.normalized * contactFactor, ForceMode2D.Impulse);
				_impactDelay = 0.1f;
				Debug.LogError(Time.time+" "+ m_contact.normal.normalized * contactFactor);
			}
		}
	}

	public void AddReward(int value)
	{
		Player.AddReward(value);
	}

	public void PlayAnimState(string animState)
	{
		m_animator.Play(animState);
	}

	public void ShowGrosYeux(bool show)
	{
		m_eyesInNight.gameObject.SetActive(show);
		GetComponent<SortingGroup>().enabled = !show;
	}
}