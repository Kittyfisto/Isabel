using System.Collections.Generic;

namespace Isabel.Speech.Synthesis
{
	public interface ITemplate
	{
		/// <summary>
		///     The gender of the voice that should be heard.
		/// </summary>
		Gender VoiceGender { get; }

		/// <summary>
		/// </summary>
		IReadOnlyDictionary<Beep, string> Beeps { get; }
	}
}