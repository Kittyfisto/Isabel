using System.Runtime.Serialization;

namespace Isabel.Speech.Synthesis
{
	[DataContract]
	public enum Gender
	{
		/// <summary>
		///     Any gender will do.
		///     The gender that is used will be decided by the speech synthesis engine.
		/// </summary>
		[EnumMember] Any,

		/// <summary>
		///     Indicates a female voice.
		/// </summary>
		[EnumMember] Female,

		/// <summary>
		///     Indicates a male voice.
		/// </summary>
		[EnumMember] Male
	}
}