using System.Runtime.Serialization;

namespace Isabel.Speech.Synthesis
{
	/// <summary>
	///     The (often system-specific) configuration for the voice synthesis engine.
	///     Mostly defines which audio device is to be used as well as volume, etc...
	/// </summary>
	[DataContract]
	public sealed class Configuration
		: IConfiguration
	{
		
	}
}