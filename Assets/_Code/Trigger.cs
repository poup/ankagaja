using System;
using UnityEngine;

namespace Assets._Code
{
	
	
	
	public class Trigger : MonoBehaviour
	{
		public event Action OnTriggerEnter;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player")&& OnTriggerEnter!=null)
			{
				OnTriggerEnter.Invoke();
			}
		}
	}
}