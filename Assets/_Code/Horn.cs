using System;
using System.Collections;
using UnityEngine;

namespace Assets._Code
{
	public class Horn : TriggerBox
	{
		[SerializeField] private int _hitToDestroy = 10;
		[SerializeField] private TriggerBox[] _loots;
		
		[SerializeField] private float _forceRatio = 10 ;
		
		[SerializeField] private bool _activeOnDash = false ;

		private int _currenthitLeft;

		protected virtual IEnumerator Start()
		{
			_currenthitLeft = 10;
			yield return base.Start();
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				var playerController = other.gameObject.GetComponent<PlayerController>();
				if(playerController!=null && (playerController.IsDashing || !_activeOnDash))
					SpwanRandomLoot();
			}
			
			if(_currenthitLeft<=0)
				Destroy(this.gameObject);
		}

		private void SpwanRandomLoot()
		{
			var randomDirection = UnityEngine.Random.insideUnitCircle.normalized *  UnityEngine.Random.Range(1.5f,8f);
			TriggerBox triggerBox = Instantiate(_loots.PickRandom(),transform.position,Quaternion.identity);
			var rb = triggerBox.GetComponent<Rigidbody2D>();
			rb.AddForce(randomDirection * _forceRatio,ForceMode2D.Impulse);
//			StartCoroutine(MoveLoot(triggerBox, 0.5f, transform.position + new Vector3(randomDirection.x, randomDirection.y,0)));
			_currenthitLeft--;
		}

//		private IEnumerator MoveLoot(TriggerBox box, float timeMs, Vector3 finalPos)
//		{
//			var initialTime = Time.time;
//			var initPos = box.transform.position;
//			var ratio = 0f;
//			while (ratio<=1)
//			{
//				ratio = (Time.time - initialTime) / timeMs;
//
//				box.transform.position = Vector3.Lerp(initPos, finalPos, ratio);
//				yield return null;
//			}
//		}
	}
}