﻿using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{

	[SerializeField] private List<BaseTrap> m_traps = new List<BaseTrap>();

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
		return m_traps.PickRandom();
	}

	public void StartTrap()
	{
		if (m_traps.Count == 0)
			return;
		var trap = GetRandomTrap();
		Debug.Log("trap started");
	}
}
