using Assets.Scripts.PlayerManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Code.UI
{
	public class PlayersFeedBackUiInGame : MonoBehaviour
	{
		[SerializeField] private Image[] _player;

		private void Awake()
		{
			foreach (var p in _player)
			{
				p.gameObject.SetActive(false);
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
					_player[activeIndex-1].gameObject.SetActive(true);
				}
			}

		}

		private void OnNewPlayer(PlayerInput newPlayer)
		{
			if (newPlayer.InputIndex-1 >= 0 && newPlayer.InputIndex-1 < _player.Length)
			{
				_player[newPlayer.InputIndex-1].gameObject.SetActive(true);
			}
		}

		private void OnPlayerLeave(PlayerInput p)
		{
			if (p.InputIndex-1 >= 0 && p.InputIndex-1 < _player.Length)
			{
				_player[p.InputIndex-1].gameObject.SetActive(false);
			}
		}

	}
}