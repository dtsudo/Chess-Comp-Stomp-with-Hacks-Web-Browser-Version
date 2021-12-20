
namespace DTLibrary
{
	/// <summary>
	/// An implementation of IKeyboard that simply represents
	/// no input (i.e. no keys are pressed).
	/// </summary>
	public class EmptyKeyboard : IKeyboard
	{
		public EmptyKeyboard()
		{
		}

		public bool IsPressed(Key key)
		{
			return false;
		}
	}
}

