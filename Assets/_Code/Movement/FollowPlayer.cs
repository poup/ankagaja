using System.Collections.Generic;
using LimProject.Maximini.Race;
using UnityEngine;

namespace Assets._Code.Movement
{
	public class FollowPlayer : MonoBehaviour
	{
		public float m_speed;
		private PlayerController m_target;

		public PlayerController Target
		{
			get { return m_target; }
			set { m_target = value; }
		}

		void FixedUpdate()
		{
			Target = LookForTarget();

			if (Target)
			{
				var direction = (Target.transform.position - transform.position).normalized;
				var move = direction.normalized * m_speed * Time.deltaTime;
				transform.position = transform.position + move;
			}
		}

		private PlayerController LookForTarget()
		{
			var characters = new List<PlayerController>(RaceManager.Instance.CharactersController.Characters);

			PlayerController chararc = null;
			var currentDistance = float.MaxValue;
			for (int i = 0; i < characters.Count; i++)
			{
				PlayerController c = characters[i];
				if (c.IsDead)
					continue;
				
				var d = (c.transform.position - transform.position).magnitude;
				if (d < currentDistance)
				{
					chararc = c;
					currentDistance = d;
				}
			}

			return chararc;
		}
	}
}