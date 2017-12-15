using System;
using UnityEngine;

namespace Assets._Code
{

	public delegate void OnTriggerEnter(GameObject triggered, Collider2D triggerer);
	
	public class Trigger : MonoBehaviour
	{
		public event OnTriggerEnter OnTriggerEnter;
		
		private void OnTriggerStay2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player") && OnTriggerEnter!=null)
			{
				OnTriggerEnter.Invoke(gameObject, other);
			}
		}
	}
}