using UnityEngine;

namespace Assets._Code.Movement
{
	public class Directionable : MonoBehaviour
	{
		public Vector2 Direction { get; set; }

		private void Start()
		{
			transform.rotation = Quaternion.FromToRotation(new Vector3(1,0,0), Direction);
		}
	}
}