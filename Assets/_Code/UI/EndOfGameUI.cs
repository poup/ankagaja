using System;
using DefaultNamespace;
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

        void Start()
        {
            m_score1.text =m_score2.text =m_score3.text = m_score4.text = "";
            try
            {
                m_score1.text = PlayersManager.Instance.Players[0].Value.ToString();
                m_score2.text = PlayersManager.Instance.Players[1].Value.ToString();
                m_score3.text = PlayersManager.Instance.Players[2].Value.ToString();
                m_score4.text = PlayersManager.Instance.Players[3].Value.ToString();
            }catch(Exception ex) {}
        }

        private void Update()
        {
            if (InputsManager.Instance.MainPlayerInput.Get.A())
            {
                foreach (var player in PlayersManager.Instance.Players)
                {
                    player.Reset();
                }
                
                FSM.Instance.GotoState<LobbyState>();
            }
        }
    }
}