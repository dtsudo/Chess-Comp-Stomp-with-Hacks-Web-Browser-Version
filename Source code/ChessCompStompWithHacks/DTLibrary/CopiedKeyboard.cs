
namespace DTLibrary
{
	using System.Collections.Generic;
	using System;

	/// <summary>
	/// CopiedKeyboard is just an easy way to make a deep copy
	/// of an IKeyboard object.  Its constructor takes an IKeyboard
	/// object in order to create a copy of the keyboard.
	/// 
	/// In general, making a copy of the IKeyboard object can
	/// be useful, since this copy is immutable and is guaranteed
	/// not to change.
	/// </summary>
	public class CopiedKeyboard : IKeyboard
	{
		private Dictionary<Key, bool> mapping;

		public CopiedKeyboard(IKeyboard keyboard)
		{
			mapping = new Dictionary<Key, bool>();
			foreach (Key key in Enum.GetValues(typeof(Key)))
			{
				mapping[key] = keyboard.IsPressed(key);
			}
		}

		public bool IsPressed(Key key)
		{
			return this.mapping[key];
		}
	}
}
