
namespace DTLibrary
{
	/// <summary>
	/// An implementation of IMouse that takes an existing IMouse object (in the constructor)
	/// and creates an IMouse implementation that's simply the same mouse input, but translated
	/// by some offset.
	/// </summary>
	public class TranslatedMouse : IMouse
	{
		private int x;
		private int y;
		private bool pressedLeft;
		private bool pressedRight;

		public TranslatedMouse(IMouse mouse, int xOffset, int yOffset)
		{
			this.x = mouse.GetX() + xOffset;
			this.y = mouse.GetY() + yOffset;
			this.pressedLeft = mouse.IsLeftMouseButtonPressed();
			this.pressedRight = mouse.IsRightMouseButtonPressed();
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
			return this.pressedLeft;
		}

		public bool IsRightMouseButtonPressed()
		{
			return this.pressedRight;
		}
	}
}

