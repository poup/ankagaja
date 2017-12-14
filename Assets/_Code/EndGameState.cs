using Plugins.Utils.FSM;

namespace Assets._Code
{
	public class EndGameState : AbstractState
	{
		public override string[] GetStaticSceneName()
		{
			return new  string[]{"GameResultUI"};
		}
	}
}