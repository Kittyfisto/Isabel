using System.Runtime.Serialization;

namespace Isabel
{
	/// <summary>
	///     The complete configuration for isabel.
	/// </summary>
	/// <remarks>
	///     The configuration is concerned with making sure that isabel works with the correct audio
	///     devices (speakers, mic, etc...), but does not configure voice commands: that is part of the
	///     template(s).
	/// </remarks>
	[DataContract]
	public sealed class Configuration
	{
		/// <summary>
		///     The configuration of the speech recognition engine.
		/// </summary>
		[DataMember]
		public Speech.Recognition.Configuration SpeechRecognitionConfiguration { get; set; }

		/// <summary>
		///     The configuration of the speech synthesis engine.
		/// </summary>
		[DataMember]
		public Speech.Recognition.Configuration SpeechSynthesisConfiguration { get; set; }
	}
}