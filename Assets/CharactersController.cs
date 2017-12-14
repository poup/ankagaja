using System.Collections.Generic;
using UnityEngine;

namespace LimProject.Maximini.Character
{
	public class CharactersController
	{

		private List<CharacterMain> _characters;

		public List<CharacterMain> Characters
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

		private void PreUpdate(CharacterMain cMain)
		{
		}

		private void InternalUpdate(CharacterMain cMain)
		{
		}

		private void PostUpdate(CharacterMain cMain)
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