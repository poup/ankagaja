using Assets.Scripts.PlayerManagement;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Code.UI
{
	public class PlayersFeedBackUiInGame : MonoBehaviour
	{
		[SerializeField] private GameObject[] _player;
		private Text[] _playerText;
		
		private void Awake()
		{
			_playerText = new Text[_player.Length];

			int i = 0;
			foreach (var p in _player)
			{
				p.SetActive(false);
				_playerText[i] = p.gameObject.GetComponentInChildren<Text>();

				i++;
			}
		}

		private void Start()
		{
			InputsManager.Instance.OnNewPlayer += OnNewPlayer;
			InputsManager.Instance.OnPlayerLeave += OnPlayerLeave;
			

			foreach (var activeIndex in InputsManager.Instance.ActiveIndex)
			{
				if (activeIndex-1 >= 0 && activeIndex-1 < _player.Length)
				{
					_player[activeIndex-1].SetActive(true);
					
				}
			}

		}

		private void Update()
		{
			for (int i = 0; i < PlayersManager.Instance.Players.Count; i++)
			{
				var player = PlayersManager.Instance.Players[i];
				_playerText[i].text = "" + player.Value;
			}
		}

		private void OnNewPlayer(PlayerInput newPlayer)
		{
			if (newPlayer.InputIndex-1 >= 0 && newPlayer.InputIndex-1 < _player.Length)
			{
				_player[newPlayer.InputIndex-1].SetActive(true);
			}
		}

		private void OnPlayerLeave(PlayerInput p)
		{
			if (p.InputIndex-1 >= 0 && p.InputIndex-1 < _player.Length)
			{
				_player[p.InputIndex-1].SetActive(false);
			}
		}

	}
}