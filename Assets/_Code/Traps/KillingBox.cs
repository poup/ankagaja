using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingBox : MonoBehaviour
{
	[SerializeField] private string m_appearanceAnimation; 
	[SerializeField] private string m_playerAnimation; 
	[SerializeField] private bool m_stopPlayerControl; 
	
	[SerializeField] private float m_timeBeforeActivation; 
	[SerializeField] private float m_activeDuration;

	[SerializeField] private Collider2D m_collider;


	private void Awake()
	{
		m_collider.enabled = false;
	}
	
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(m_timeBeforeActivation);
		m_collider.enabled = true;
		if(m_activeDuration > 0)
			StartCoroutine(StopDelayed());
	}

	private IEnumerator StopDelayed()
	{
		yield return new WaitForSeconds(m_activeDuration);
		m_collider.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var player = other.gameObject;
			Debug.Log(" hit by something");
		}
	}
}
