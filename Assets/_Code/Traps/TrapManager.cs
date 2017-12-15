using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

	[SerializeField] private List<BaseTrap> m_traps = new List<BaseTrap>();

	private static TrapManager m_instance;
	
	public static TrapManager Instance
	{
		get { return m_instance; }
	}

	public void Awake()
	{
		m_instance = this;
	}

	public BaseTrap GetRandomTrap()
	{
		return m_traps.PickRandom();
	}

	public void StartTrap()
	{
		if (m_traps.Count == 0)
			return;
		var trap = GetRandomTrap();
		Instantiate(trap, Vector3.zero, Quaternion.identity, transform);
		
		Debug.Log("trap started");
	}
}
