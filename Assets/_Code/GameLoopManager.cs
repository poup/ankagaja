using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using States;

namespace Assets._Code
{
	public class GameLoopManager
	{
		private static GameLoopManager _instance;

		public  static GameLoopManager Instance
		{
			get
			{
				if(_instance==null)
					_instance = new GameLoopManager();
				return _instance;
			}
		}

		private int _loopCount;

		public void Reset()
		{
			_loopCount = 0;
		}

		public void IncLoopCount()
		{
			_loopCount++;
		}

		public void RoomEnded()
		{
			if (_loopCount < 10)
			{
				FSM.Instance.GotoState<GameState>(new List<string>() {"1"}, true);
				_loopCount++;
			}
			else
			{
				FSM.Instance.GotoState<EndGameState>(new List<string>(), true);
			}
		}
		
	}
}