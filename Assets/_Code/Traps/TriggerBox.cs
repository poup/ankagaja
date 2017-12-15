using System.Collections;
using Assets._Code;
using UnityEngine;

public class TriggerBox : Trigger
{
	[SerializeField] private string m_appearanceAnimation; 
	[SerializeField] private string m_playerAnimation; 
	[SerializeField] private bool m_stopPlayerControl; 
	
	[SerializeField] private float m_timeBeforeActivation; 
	[SerializeField] private float m_activeDuration;
	[SerializeField] private float m_waitBeforeDelete;

	[SerializeField] private Collider2D m_collider;


	private void Awake()
	{
		m_collider.enabled = false;
	}
	
	
	
	protected virtual IEnumerator Start()
	{
		PlayAnimation();
		yield return new WaitForSeconds(m_timeBeforeActivation);
		m_collider.enabled = true;
		if(m_activeDuration > 0)
			StartCoroutine(StopDelayed());
	}

	private void PlayAnimation()
	{
		// TODO
	}

	private IEnumerator StopDelayed()
	{
		yield return new WaitForSeconds(m_activeDuration);
		m_collider.enabled = false;
		yield return new WaitForSeconds(m_waitBeforeDelete);
		Destroy(gameObject);
	}

	private void OnDrawGizmos()
	{
		if (m_collider == null)
			return;
		var color = Gizmos.color;
		if (m_collider.enabled)
		{
			Gizmos.color = Color.red;
		}
		else
		{
			Gizmos.color = Color.green;
		}

		if (m_collider is BoxCollider2D)
		{
			var c = m_collider as BoxCollider2D;
			Gizmos.DrawWireCube(c.transform.position + c.offset.ToVector3(), c.size);
		}
		if (m_collider is CircleCollider2D)
		{
			var c = m_collider as CircleCollider2D;
			Gizmos.DrawWireSphere(c.transform.position + c.offset.ToVector3(), c.radius);
		}
		if (m_collider is PolygonCollider2D)
		{
			var c = m_collider as PolygonCollider2D;
			
			Vector2[] points = c.points;
			var origin = transform.position + c.offset.ToVector3();
 
			for(int i = 0; i < points.Length-1; i++)
			{
				Gizmos.DrawLine(origin + points[i].ToVector3(), origin + points[i+1].ToVector3());
			}
			Gizmos.DrawLine(origin + points[points.Length - 1].ToVector3(), origin + points[0].ToVector3());
		}
		Gizmos.color = color;
	}
}
