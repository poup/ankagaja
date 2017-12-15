using System.Collections;
using UnityEngine;

namespace Assets._Code
{
	public class Loot : TriggerBox
	{
		[SerializeField] public int value;

		[SerializeField] private Sprite[] m_sprites;
		[SerializeField] private SpriteRenderer m_renderer;
		

		private IEnumerator Start()
		{
			m_renderer.sprite = m_sprites.PickRandom();
			OnTriggerEnter += OnTrigger;
			yield return base.Start();
		}

		private void OnTrigger(GameObject triggered, Collider2D triggerer)
		{
			var playerController = triggerer.transform.GetComponent<PlayerController>();
			if (playerController != null)
			{
				playerController.AddReward(value);
				StartCoroutine(AutoDestroy());
			}
		}

		private IEnumerator AutoDestroy()
		{
			// TODO animation ???
			yield return null;
			Destroy(this.gameObject);
		}
	}
}