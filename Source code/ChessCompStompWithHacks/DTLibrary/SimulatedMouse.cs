
namespace DTLibrary
{
	public class SimulatedMouse : IMouse
	{
		private int x;
		private int y;
		private bool isLeftMouseButtonPressed;
		private bool isRightMouseButtonPressed;

		public SimulatedMouse(int x, int y, bool isLeftMouseButtonPressed, bool isRightMouseButtonPressed)
		{
			this.x = x;
			this.y = y;
			this.isLeftMouseButtonPressed = isLeftMouseButtonPressed;
			this.isRightMouseButtonPressed = isRightMouseButtonPressed;
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
			return this.isLeftMouseButtonPressed;
		}

		public bool IsRightMouseButtonPressed()
		{
			return this.isRightMouseButtonPressed;
		}
	}
}
