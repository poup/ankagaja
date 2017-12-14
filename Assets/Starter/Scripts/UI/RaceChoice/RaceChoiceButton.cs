using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceChoiceButton : MonoBehaviour
{

	[SerializeField]
	private Image _normalBg;
	[SerializeField]
	private Image _selectedBg;
	[SerializeField]
	private Text _text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string Name
	{
		get { return _text.text; }
		set { _text.text = value; }
	}

	public void Select(bool value)
	{
		_normalBg.gameObject.SetActive(!value);
		_selectedBg.gameObject.SetActive(value);
	}
}
