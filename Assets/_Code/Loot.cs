using UnityEditor;
using UnityEngine;

namespace Assets._Code
{
	public class Loot : MonoBehaviour
	{
		[SerializeField] private int value;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				var playerController = other.transform.parent.GetComponent<PlayerController>();
				if (playerController != null)
				{

					playerController.AddReward(value);
					Destroy(this.gameObject);
				}
			}
		}
	}
}