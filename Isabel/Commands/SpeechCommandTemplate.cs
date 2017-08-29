using System.Runtime.Serialization;
using Isabel.Speech.Recognition;

namespace Isabel.Commands
{
	[DataContract]
	public sealed class SpeechCommandTemplate
		: AbstractCommandTemplate
	{
		/// <summary>
		///     The word, sentence, etc... to be spoken by a <see cref="ISpeechSynthesisEngine" />.
		/// </summary>
		[DataMember]
		public string Speech { get; set; }

		/// <summary>
		///     Whether or not the command is executed asynchronously.
		/// </summary>
		/// <remarks>
		///     By default, is set to false: No other command may be executed until the speech is finished.
		/// </remarks>
		[DataMember]
		public bool IsAsync { get; set; }
	}
}