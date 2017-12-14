using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

	[SerializeField] private List<BaseTrap> m_traps = new List<BaseTrap>();

	private readonly System.Random m_random = new System.Random();

	public static TrapManager m_instance;
	
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
		var index = m_random.Next(m_traps.Count);
		return m_traps[index];
	}

	public void StartTrap()
	{
		if (m_traps.Count == 0)
			return;
		var trap = GetRandomTrap();
		Debug.Log("trap started");
	}
}
