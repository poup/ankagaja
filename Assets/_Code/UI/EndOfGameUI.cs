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

                foreach (var player in PlayersManager.Instance.Players)
                {
                    if (player.Input.InputIndex == 1)
                        m_score1.text = player.Value.ToString();
                    if (player.Input.InputIndex == 2)
                        m_score2.text = player.Value.ToString();
                    if (player.Input.InputIndex == 3)
                        m_score3.text = player.Value.ToString();
                    if (player.Input.InputIndex == 4)
                        m_score4.text = player.Value.ToString();
                }
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