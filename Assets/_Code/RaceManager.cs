﻿using System.Collections.Generic;
using System.Linq;
using Assets._Code;
using DefaultNamespace;
using LimProject.Maximini.Character;
using States;
using UnityEngine;
using UnityEngine.UI;

namespace LimProject.Maximini.Race
{
	public class RaceManager : MonoBehaviour
	{
		#region Singleton

		private static RaceManager s_instance;

		public static RaceManager Instance
		{
			get { return s_instance; }
		}

		#endregion

		public PlayerController[] _characterPrefabs;
		
		private LevelData _data;

		public LevelData Data
		{
			get { return _data; }
		}

//    [SerializeField]
//    private StartCountDown _countDownUI;    
//    [SerializeField]
//    private MultipleChoiceButtons _restartOrLeaveUI;    
//		[SerializeField] private Button _openRestartOrLeaveButton;

		private CharactersController _charactersController;

		public CharactersController CharactersController
		{
			get { return _charactersController; }
		}

		private float _currentTime;


//    private RaceStateEnum _state = RaceStateEnum.Intro;

		void Awake()
		{
			s_instance = this;
		}

		// Use this for initialization
		void Start()
		{
			_charactersController = new CharactersController();

			
			_data = FindObjectOfType<LevelData>();
			
			InitializePlayers();
			//InputsManager.Instance.AutoUpdate = false;

//      _state = RaceStateEnum.Intro;
//      _countDownUI.OnCountDownFinished += () => _state = RaceStateEnum.Run;
//      _countDownUI.Reset(3);

//      _openRestartOrLeaveButton.onClick.AddListener(() =>
//        _restartOrLeaveUI.gameObject.SetActive(!_restartOrLeaveUI.gameObject.activeSelf));

			_currentTime = 0;
			_data._endTrigger.OnTriggerEnter += OnEnd;
		}

		private void OnEnd(GameObject triggered, Collider2D triggerer)
		{
			TrapManager.Instance.RoomEnded();
			GameLoopManager.Instance.RoomEnded();
		}

		void Update()
		{
			_currentTime += Time.deltaTime;
			if (_currentTime > _data._time && _data._door.activeInHierarchy)
			{
				_data._door.SetActive(false);
			}
			
//      if (InputsManager.Instance.MainPlayerInput.IsPresent && InputsManager.Instance.MainPlayerInput.Get.StartDown())
//      {
//        _restartOrLeaveUI.gameObject.SetActive(!_restartOrLeaveUI.gameObject.activeSelf);
//      }

//      switch (_state)
//      {
//        case RaceStateEnum.Intro:
//          break;
//        case RaceStateEnum.Run:
//          {
//            InputsManager.Instance.CustomPreUpdate();

			_charactersController.CustomUpdate();
//          }
//          break;
//        case RaceStateEnum.End:
//            break;
//      }
		}

		private void FixedUpdate()
		{
//      switch (_state)
//      {
//        case RaceStateEnum.Intro:
//          break;
//        case RaceStateEnum.Run:
//        {
			_charactersController.CustomFixedUpdate();
//        }
//          break;
//        case RaceStateEnum.End:
//          break;
//      }
		}

		private void LateUpdate()
		{
		}

		private void OnDestroy()
		{
			_charactersController.OnDestroy();
			s_instance = null;
		}

		public void Restart()
		{
//      FSM.Instance.RestartState();
		}

		public void Leave()
		{
			FSM.Instance.GotoState<LobbyState>();
		}

		private void InitializePlayers()
		{
			int i = 0;
			var characters = new List<PlayerController>();

//			if (PlayersManager.Instance.Players.Count == 0)
				; //InputsManager.Instance.ForceCreateMainPlayer();

			var players = PlayersManager.Instance.Players;
			for (var index = 0; index < players.Count; index++)
			{
				var p = players[index];
				var prefab = _characterPrefabs[index % _characterPrefabs.Length];
//_data._StartPoints
				//var ship = PrefabUtility.InstantiatePrefab(_shipPrefab);

				var character = Instantiate(prefab);

				character.transform.SetParent(transform);
				character.transform.SetParent(null);
//        HandlerManager.Instance.CreateHandler(character,PatternEnum.Default);

				character.Input = p.Input;
				character.Player = p;

//        controller.Color = p.Color;

				character.transform.parent = _data._characterParent;
				character.transform.position = _data._spawPoints[i].position;

				characters.Add(character);

				i++;
			}

			_charactersController.Characters = characters;
//      _camera.SetPlayerToFollow(characters.Select(c => (ICameraTarget)c).ToList());
		}

		public void CheckAllDead()
		{
			if(_charactersController.Characters.All(c => !c.gameObject.activeInHierarchy))
				FSM.Instance.GotoState<EndGameState>(new List<string>(), true);
		}
	}
}