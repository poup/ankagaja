using States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Code.UI
{
	public class EndOfGameUI : MonoBehaviour
	{
		public Text m_score1;
		public Text m_score2;
		public Text m_score3;
		public Text m_score4;
		
		private void Update()
		{
			if (InputsManager.Instance.MainPlayerInput.Get.A())
			{
				FSM.Instance.GotoState<LobbyState>();
			}
		}
	}
}