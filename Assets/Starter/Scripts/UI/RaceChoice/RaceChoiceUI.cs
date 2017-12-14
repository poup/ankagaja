using System.Collections;
using System.Collections.Generic;
using Assets._Code;
using States;
using UnityEngine;

public class RaceChoiceUI : MonoBehaviour
{

	[SerializeField]
	private string[] _raceNames;
	
	[SerializeField]
	private RaceChoiceButton _raceButtonPrefab;
	
	private List<RaceChoiceButton> _raceButtons = new List<RaceChoiceButton>();
	private int _currentSelectedRaceIndex = 0;

	private float _lastChangeTime = 0;

	// Use this for initialization
	void Start () {
		foreach (var raceName in _raceNames)
		{
			var raceChoiceButton = GameObject.Instantiate(_raceButtonPrefab,transform);
			_raceButtons.Add(raceChoiceButton);
			raceChoiceButton.Name = raceName;
		}
		
		_raceButtons[0].Select(true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckSelectionChange();
	}

	private void CheckSelectionChange()
	{

		if (Time.time - _lastChangeTime <= 0.5)
			return;
		
		var mainPlayerInput = InputsManager.Instance.MainPlayerInput;
		if (mainPlayerInput.IsPresent)
		{
			var newRaceIndex = _currentSelectedRaceIndex;
			if (mainPlayerInput.Get.V1() < -0.5)
			{
				newRaceIndex--;
				if (newRaceIndex < 0)
					newRaceIndex = _raceButtons.Count - 1;
			}
			if (mainPlayerInput.Get.V1() > 0.5)
			{
				newRaceIndex++;
				if (newRaceIndex >= _raceButtons.Count)
					newRaceIndex = 0;
			}

			if (mainPlayerInput.Get.V1() > -0.5 && mainPlayerInput.Get.V1() < 0.5)
				_lastChangeTime = 0f;

			if (_currentSelectedRaceIndex != newRaceIndex)
			{
				_raceButtons[_currentSelectedRaceIndex].Select(false);
				_raceButtons[newRaceIndex].Select(true);
				_currentSelectedRaceIndex = newRaceIndex;

				_lastChangeTime = Time.time;
			}

			if (mainPlayerInput.Get.ADown())
			{
				GameLoopManager.Instance.Reset();
				FSM.Instance.GotoState<GameState>(
					new List<string>(){	_raceNames[_currentSelectedRaceIndex]	}
				);
			}
		}
	}
}
