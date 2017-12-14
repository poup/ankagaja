using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private Rigidbody m_rigidBody;
	[SerializeField] private float m_speedMax;
	
	private bool m_isJump; 

	
	
	// Update is called once per frame
	void Update ()
	{
		var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
		
		m_rigidBody.velocity = new Vector3(x, 0, y) * m_speedMax;
	}
}
