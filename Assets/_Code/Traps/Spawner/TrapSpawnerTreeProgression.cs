using System.Collections;
using System.Collections.Generic;
using Assets._Code.Movement;
using UnityEngine;

namespace Assets._Code.Spawner
{
	
	
	public class TrapSpawnerTreeProgression : TrapSpawner
	{
		[SerializeField] protected int m_lavaCellCount = 15;
		[SerializeField] protected float m_timeBetweenSpawn = 0.5f;
		
		[Space(10)]
		[SerializeField] protected TrapActions.TrapActionsType m_actionOnTrigger;
		
		
		protected Vector2 m_direction;
		//private List<TriggerBox> m_heads = new List<TriggerBox>();
		private TriggerBox m_head;
		
		private List<Vector2> directions = new List<Vector2>(){new Vector2(1,0), new Vector2(-1,0), new Vector2(0,1), new Vector2(0,-1)};
		

		
		protected override IEnumerator Spawn()
		{
			var obj = SpawnOne();
			m_direction = directions.PickRandom();
			obj.OnTriggerEnter += ActivateKillingBox;
			ApplyDirection(obj);
			m_head = obj;
			m_lavaCellCount--;

			yield return new WaitForSeconds(m_timeBetweenSpawn);

			while (m_lavaCellCount>0)
			{
				var newObj = SpawnOne();
				m_direction = directions.PickRandom();
				newObj.transform.position = m_head.transform.position + m_direction.ToVector3();
				newObj.OnTriggerEnter += ActivateKillingBox;
				ApplyDirection(newObj);
				m_head = newObj;
				m_lavaCellCount--;
				yield return new WaitForSeconds(m_timeBetweenSpawn);
			}
			
			
		}
		
		private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
		{
			TrapActions.DoAction(this, m_actionOnTrigger,triggered, triggerer );
		}
		
		private void ApplyDirection(TriggerBox obj)
		{
				var direction = obj.GetComponent<Directionable>();
				if (direction)
				{
					direction.Direction = m_direction;
				}   
			}
		}
		
}