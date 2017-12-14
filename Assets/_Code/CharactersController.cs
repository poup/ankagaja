using System.Collections.Generic;
using UnityEngine;

namespace LimProject.Maximini.Character
{
	public class CharactersController
	{

		private List<PlayerController> _characters;

		public List<PlayerController> Characters
		{
			get { return _characters; }
			set { _characters = value; }
		}

		public CharactersController()
		{
		}

		public void CustomFixedUpdate()
		{
		}

		public void CustomUpdate()
		{
		}

		private void PreUpdate(PlayerController cMain)
		{
		}

		private void InternalUpdate(PlayerController cMain)
		{
		}

		private void PostUpdate(PlayerController cMain)
		{
		}


		#region Force

		#endregion

		public void OnDestroy()
		{
			foreach (var character in _characters)
			{
				GameObject.Destroy(character);
			}
		}
	}
}