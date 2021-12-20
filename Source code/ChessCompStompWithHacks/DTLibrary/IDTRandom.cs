
namespace DTLibrary
{
	/// <summary>
	/// An interface representing a (pseudo) random number generator.
	/// </summary>
	public interface IDTRandom
	{
		/// <summary>
		/// Modifies the seed of the random number generator.
		/// 
		/// The exact behavior of this function is up to the implementation.
		/// e.g. the implementation can ignore the input value, or it can replace its
		/// current seed with the new input value, or it can augment the current
		/// seed with the new input value.
		/// </summary>
		void AddSeed(int i);

		/// <summary>
		/// Returns an integer in [0, i)
		/// </summary>
		int NextInt(int i);

		/// <summary>
		/// Generates a random boolean value (either true or false, both with 50% probability)
		/// </summary>
		/// <returns></returns>
		bool NextBool();
	}
}
