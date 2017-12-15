using System;
using UnityEngine;

namespace Assets._Code
{

	public delegate void OnTriggerEnter(GameObject triggered, Collider2D triggerer);
	
	public class Trigger : MonoBehaviour
	{
		public event OnTriggerEnter OnTriggerEnter;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player")&& OnTriggerEnter!=null)
			{
				OnTriggerEnter.Invoke(gameObject, other);
			}
		}
	}
}