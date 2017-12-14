using States;
using UnityEngine;

namespace Assets._Code.UI
{
	public class EndOfGameUI : MonoBehaviour
	{
		private void Update()
		{
			if (InputsManager.Instance.MainPlayerInput.Get.A())
			{
				FSM.Instance.GotoState<LobbyState>();
			}
		}
	}
}