
namespace DTLibrary
{
	/// <summary>
	/// An interface that marks an implementation of IDTRandom as completely deterministic.
	/// 
	/// Deterministic is defined to mean that the implementation will always
	/// return the same values given the same seed and sequence of function calls.
	/// 
	/// This means that an instance of IDTDeterministicRandom must behave identically
	/// across a variety of dimensions.
	/// 
	/// For instance:
	/// * Two instances on different computers (with the same seed and function calls)
	///   must return the same values.
	/// * Two instances being executed at different times (with the same seed and function calls)
	///   must return the same values.
	/// * Two instances being executed on different versions of C# (with the same seed and function calls)
	///   must return the same values.
	/// * Two instances being executed on different operating systems (with the same seed and function calls)
	///   must return the same values.
	/// </summary>
	public interface IDTDeterministicRandom : IDTRandom
	{
		/// <summary>
		/// Resets the random number generator.
		/// 
		/// When reset, the random number generator should return the same sequence of outputs
		/// as a newly created random number generator.
		/// </summary>
		void Reset();

		string SerializeToString();

		void DeserializeFromString(string str);
	}
}
