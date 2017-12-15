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
			obj.transform.position = new Vector3((int)obj.transform.position.x, (int)obj.transform.position.y,0);
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
	
//	public class TrapSpawnerTreeProgression : TrapSpawner
//	{
//		[SerializeField] protected int m_lavaCellCount = 15;
//		[SerializeField] protected float m_timeBetweenSpawn = 0.5f;
//
//		[Space(10)] [SerializeField] protected TrapActions.TrapActionsType m_actionOnTrigger;
//
//		[Space(10)] [SerializeField] private float m_width;
//		[SerializeField] private float m_height;
//
//
//		protected Vector2 m_direction;
//
//		//private List<TriggerBox> m_heads = new List<TriggerBox>();
//		private TriggerBox m_head;
//
//		private List<Vector2> directions =
//			new List<Vector2>() {new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)};
//
//		private readonly List<Vector2> m_allCells = new List<Vector2>();
//
//
//		protected override IEnumerator Spawn()
//		{
//			InitCells();
//			
//			var obj = SpawnOne();
//			m_direction = directions.PickRandom();
//			obj.OnTriggerEnter += ActivateKillingBox;
//			ApplyDirection(obj);
//			m_head = obj;
//			m_lavaCellCount--;
//
//			obj.transform.position = new Vector3((int)obj.transform.position.x, (int)obj.transform.position.y,0);
//			m_allCells.Remove(obj.transform.position.ToVector2());
//			yield return new WaitForSeconds(m_timeBetweenSpawn);
//
//			while (m_lavaCellCount > 0)
//			{
//				var newObj = SpawnOne();
//				if(!FindPosition( ))
//					yield break;
//				
//				newObj.transform.position = m_head.transform.position + m_direction.ToVector3();
//				newObj.OnTriggerEnter += ActivateKillingBox;
//				ApplyDirection(newObj);
//				m_head = newObj;
//				m_lavaCellCount--;
//				m_allCells.Remove(newObj.transform.position.ToVector2());
//				yield return new WaitForSeconds(m_timeBetweenSpawn);
//			}
//		}
//
//		private void ActivateKillingBox(GameObject triggered, Collider2D triggerer)
//		{
//			TrapActions.DoAction(this, m_actionOnTrigger, triggered, triggerer);
//		}
//
//		private void ApplyDirection(TriggerBox obj)
//		{
//			var direction = obj.GetComponent<Directionable>();
//			if (direction)
//			{
//				direction.Direction = m_direction;
//			}
//		}
//
//		private bool FindPosition( )
//		{
//			m_direction = directions.PickRandom();
//			for (int i = 0; i < 10; i++)
//			{
//				var vector2 = m_head.transform.position + m_direction.ToVector3();
//				if (m_allCells.Contains(vector2))
//					return true;
//			}
//
//			return false;
//
//		}
//
//		public void InitCells()
//		{
//			m_allCells.Clear();
//
//			var maxX = Mathf.CeilToInt(m_width);
//			var maxY = Mathf.CeilToInt(m_height);
//			for (int x = 0; x < maxX; ++x)
//			for (int y = 0; y < maxY; ++y)
//			{
//				m_allCells.Add(new Vector2(x - (m_width/2), y-(m_height/2)) + transform.position.ToVector2());
//			}
//		}
//	}
		
}