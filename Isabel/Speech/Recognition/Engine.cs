using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     Defines which speech recognition engine shall be used.
	/// </summary>
	[DataContract]
	public enum Engine
	{
		[EnumMember] Windows,

		[EnumMember] CMUSphinx
	}
}