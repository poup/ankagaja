using System;
using System.Collections;
using LimProject.Maximini.Race;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets._Code
{
	public class TrapActions
	{

		public enum TrapActionsType
		{
			None,
			HurtAndDisapear,
			HurnAndContinue,
			LootAndDisapear
		}

		public static void DoAction(TrapSpawner spawner, TrapActionsType action, GameObject triggered, Collider2D triggerer)
		{
			switch (action)
			{
				case TrapActionsType.None :
					break;
				case TrapActionsType.HurtAndDisapear:
					HurtAndDisapear(spawner, triggered, triggerer);
					break;
				case TrapActionsType.HurnAndContinue:
					HurnAndContinue(spawner, triggered, triggerer);
					break;
				case TrapActionsType.LootAndDisapear:
					LootAndDisapear(spawner, triggered, triggerer);
					break;
				default:
					throw new ArgumentOutOfRangeException("action", action, null);
			}
		}

		private static void HurtAndDisapear(TrapSpawner spawner, GameObject triggered, Collider2D triggerer)
		{
			var player = triggerer.GetComponent<PlayerController>();
			if (player != null)
			{
				player.AddReward(- RaceManager.Instance.Data._pointLostForDeath);
				player.gameObject.SetActive(false);
				player.IsDead = true;
				player.StartCoroutine(WIP_Reactivate(player.gameObject));
            
				player.PlayAnimState(spawner.m_deadStateName);

				RaceManager.Instance.CheckAllDead();
			}
			
			GameObject.Destroy(triggered.gameObject);
		}

		private static void HurnAndContinue(TrapSpawner spawner,GameObject triggered, Collider2D triggerer)
		{
			var player = triggerer.GetComponent<PlayerController>();
			if (player != null)
			{
				player.AddReward(- RaceManager.Instance.Data._pointLostForDeath);
				player.gameObject.SetActive(false);
				player.IsDead = true;
				player.StartCoroutine(WIP_Reactivate(player.gameObject));
            
				player.PlayAnimState(spawner.m_deadStateName);
				RaceManager.Instance.CheckAllDead();
			}
		}
		
		private static void LootAndDisapear(TrapSpawner spawner,GameObject triggered, Collider2D triggerer)
		{
			var playerController = triggerer.transform.GetComponent<PlayerController>();
			var lootInfo = triggered.GetComponent<Loot>();
			if (playerController && lootInfo)
			{
				playerController.AddReward(lootInfo.value);
				//StartCoroutine(AutoDestroy());
			}
		}
		
		private static IEnumerator WIP_Reactivate(GameObject go)
		{
			yield return new WaitForSeconds(0.1f);
			go.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.1f);
			go.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.1f);
			go.gameObject.SetActive(true);
		}
		
	}
}