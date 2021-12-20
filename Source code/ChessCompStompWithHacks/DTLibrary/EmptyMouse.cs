
namespace DTLibrary
{
	/// <summary>
	/// An implementation of IMouse that simply represents
	/// no input.
	/// </summary>
	public class EmptyMouse : IMouse
	{
		public EmptyMouse()
		{
		}

		public int GetX()
		{
			return 0;
		}

		public int GetY()
		{
			return 0;
		}

		public bool IsLeftMouseButtonPressed()
		{
			return false;
		}

		public bool IsRightMouseButtonPressed()
		{
			return false;
		}
	}
}
