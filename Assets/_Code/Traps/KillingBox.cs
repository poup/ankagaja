using System.Collections;
using UnityEngine;

public class KillingBox : MonoBehaviour
{
	[SerializeField] private string m_appearanceAnimation; 
	[SerializeField] private string m_playerAnimation; 
	[SerializeField] private bool m_stopPlayerControl; 
	
	[SerializeField] private float m_timeBeforeActivation; 
	[SerializeField] private float m_activeDuration;
	[SerializeField] private float m_waitBeforeDelete;

	[SerializeField] private Collider2D m_collider;
	[SerializeField] private HitEffect m_hitEffect;


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
		yield return new WaitForSeconds(m_waitBeforeDelete);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			var player = other.gameObject;
			player.GetComponent<PlayerController>().OnEffect(m_hitEffect);
		}
	}

	private void OnDrawGizmos()
	{
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
			Gizmos.DrawWireCube(c.transform.position, c.size);
		}
		if (m_collider is CircleCollider2D)
		{
			var c = m_collider as CircleCollider2D;
			Gizmos.DrawWireSphere(c.transform.position, c.radius);
		}
		if (m_collider is PolygonCollider2D)
		{
			var c = m_collider as PolygonCollider2D;
			
			Vector2[] points = c.points;
 
			// for every point (except for the last one), draw line to the next point
			for(int i = 0; i < points.Length-1; i++)
			{
				Gizmos.DrawLine(transform.position + points[i].ToVector3(), transform.position + points[i+1].ToVector3());
			}
			// for polygons, close with the last segment
			Gizmos.DrawLine(transform.position + points[points.Length - 1].ToVector3(), transform.position + points[0].ToVector3());
		}
		Gizmos.color = color;
	}
}
