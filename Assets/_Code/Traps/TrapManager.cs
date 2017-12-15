using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

	[SerializeField] private List<BaseTrap> m_traps = new List<BaseTrap>();
	private List<BaseTrap> m_trapsForRoom = new List<BaseTrap>();

	private static TrapManager m_instance;
	
	public static TrapManager Instance
	{
		get { return m_instance; }
	}

	public void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		ResetAvailableTraps();
	}

	public BaseTrap GetRandomTrap()
	{
		if(m_trapsForRoom.Count == 0)
			ResetAvailableTraps();
		
		return m_trapsForRoom.PickAndRemoveRandom();
	}

	public void StartTrap()
	{
		if (m_trapsForRoom.Count == 0)
			return;
		var trap = GetRandomTrap();
		Instantiate(trap, Vector3.zero, Quaternion.identity, transform);
		
		Debug.Log("trap started");
	}


	public void RoomEnded()
	{
		ResetAvailableTraps();
	}

	private void ResetAvailableTraps()
	{
		m_trapsForRoom = new List<BaseTrap>(m_traps);
	}
}
