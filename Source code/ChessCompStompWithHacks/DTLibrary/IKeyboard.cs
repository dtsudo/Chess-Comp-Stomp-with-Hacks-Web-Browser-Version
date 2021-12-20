
namespace DTLibrary
{
	public enum Key
	{
		A, B, C, D, E, F, G, H, I, J, K, L, M,
		N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
		Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine,
		UpArrow, DownArrow, LeftArrow, RightArrow,
		Delete, Backspace, Enter, Shift, Space,
		Esc
	}

	public interface IKeyboard
	{
		bool IsPressed(Key key);
	}
}
