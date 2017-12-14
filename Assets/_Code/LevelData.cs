using UnityEngine;

namespace Assets._Code
{
	public class LevelData : MonoBehaviour
	{
		[SerializeField] public Transform[] _spawPoints;
		
		[SerializeField] public GameObject[] _lootsPrefabs;
		[SerializeField] public int  _lootsCount;

		[SerializeField] public Vector2 _minLimit = new Vector2(-7.5f,-5.5f);
		[SerializeField] public Vector2 _maxLimit= new Vector2(7.5f,5.5f);

		[SerializeField] public GameObject _door;
		
		[SerializeField] public int _time;

		[SerializeField] public Trigger _endTrigger;
		
		[SerializeField] public Transform _collectibleParent;
		[SerializeField] public Transform _characterParent;
		
		





	}
}