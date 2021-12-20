
namespace DTLibrary
{
	/// <summary>
	/// CopiedMouse is just an easy way to make a deep copy
	/// of an IMouse object.  Its constructor takes an IMouse
	/// object in order to create a copy of the mouse.
	/// 
	/// In general, making a copy of the IMouse object can
	/// be useful, since this copy is immutable and is guaranteed
	/// not to change.
	/// </summary>
	public class CopiedMouse : IMouse
	{
		private int x;
		private int y;
		private bool leftMouse;
		private bool rightMouse;

		public CopiedMouse(IMouse mouse)
		{
			this.x = mouse.GetX();
			this.y = mouse.GetY();
			this.leftMouse = mouse.IsLeftMouseButtonPressed();
			this.rightMouse = mouse.IsRightMouseButtonPressed();
		}

		public int GetX()
		{
			return this.x;
		}

		public int GetY()
		{
			return this.y;
		}

		public bool IsLeftMouseButtonPressed()
		{
			return this.leftMouse;
		}

		public bool IsRightMouseButtonPressed()
		{
			return this.rightMouse;
		}
	}
}
