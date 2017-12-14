using System.Collections.Generic;
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

		public CharacterMain _characterPrefab;

//    [SerializeField]
//    private StartCountDown _countDownUI;    
//    [SerializeField]
//    private MultipleChoiceButtons _restartOrLeaveUI;    
//		[SerializeField] private Button _openRestartOrLeaveButton;

		private CharactersController _charactersController;


//    private RaceStateEnum _state = RaceStateEnum.Intro;

		void Awake()
		{
			s_instance = this;
		}

		// Use this for initialization
		void Start()
		{
			_charactersController = new CharactersController();

			InitializePlayers();
			//InputsManager.Instance.AutoUpdate = false;

//      _state = RaceStateEnum.Intro;
//      _countDownUI.OnCountDownFinished += () => _state = RaceStateEnum.Run;
//      _countDownUI.Reset(3);

//      _openRestartOrLeaveButton.onClick.AddListener(() =>
//        _restartOrLeaveUI.gameObject.SetActive(!_restartOrLeaveUI.gameObject.activeSelf));
		}

		void Update()
		{
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
			var characters = new List<CharacterMain>();

			if (PlayersManager.Instance.Players.Count == 0)
				; //InputsManager.Instance.ForceCreateMainPlayer();

			foreach (var p in PlayersManager.Instance.Players)
			{
				//_data._StartPoints
				//var ship = PrefabUtility.InstantiatePrefab(_shipPrefab);

				var character = Instantiate(_characterPrefab);

				character.transform.SetParent(transform);
				character.transform.SetParent(null);
//        HandlerManager.Instance.CreateHandler(character,PatternEnum.Default);

				character.Input = p.Input;
//        controller.Color = p.Color;

//        character.transform.position = _data.StartPoint.position;

				characters.Add(character);

				i++;
			}

			_charactersController.Characters = characters;
//      _camera.SetPlayerToFollow(characters.Select(c => (ICameraTarget)c).ToList());
		}
	}
}