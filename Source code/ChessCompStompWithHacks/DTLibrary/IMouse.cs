
namespace DTLibrary
{
	public interface IMouse
	{
		int GetX();
		int GetY();

		/// <summary>
		/// Note that for simplicity, this function refers to the left mouse button,
		/// although technically, this references the primary button of the mouse.
		/// 
		/// For a left-handed mouse, the primary button is actually the right button.
		/// </summary>
		bool IsLeftMouseButtonPressed();

		/// <summary>
		/// Note that for simplicity, this function refers to the right mouse button,
		/// although technically, this references the secondary button of the mouse.
		/// 
		/// For a left-handed mouse, the secondary button is actually the left button.
		/// </summary>
		bool IsRightMouseButtonPressed();
	}
}
