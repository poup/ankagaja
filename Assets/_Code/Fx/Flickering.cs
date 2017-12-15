using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
	public float flickerForce;
	public float flickerMinTime;
	public float flickerMaxTime;
	private float m_baseAlpha;
	private SpriteRenderer m_sprite;

	void Awake()
	{
		m_sprite = GetComponent<SpriteRenderer>();
		m_baseAlpha = m_sprite.color.a;

		StartFlicker();
	}

	private void StartFlicker()
	{
		StartCoroutine(Flicker());
	}

	private IEnumerator Flicker()
	{
		var color = m_sprite.color;
		color.a = m_baseAlpha + UnityEngine.Random.Range(-flickerForce, flickerForce);
		m_sprite.color = color;

		var time = Random.Range(flickerMinTime, flickerMaxTime);
		yield return new WaitForSeconds(time);
		StartFlicker();
	}
}
