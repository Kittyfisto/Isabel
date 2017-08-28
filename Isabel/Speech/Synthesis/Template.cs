using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Isabel.Speech.Synthesis
{
	/// <summary>
	///     A template for the voice synthesis engine that defines the type of voice which is to be used as well as the
	///     specific sounds that are used (when no voice output is desired).
	/// </summary>
	[DataContract]
	public sealed class Template
		: ITemplate
	{
		public Template()
		{
			Beeps = new Dictionary<Beep, string>();
		}

		/// <summary>
		///     The gender of the voice that should be heard.
		/// </summary>
		[DataMember]
		public Gender VoiceGender { get; set; }

		/// <summary>
		/// </summary>
		[DataMember]
		public Dictionary<Beep, string> Beeps { get; set; }

		IReadOnlyDictionary<Beep, string> ITemplate.Beeps => Beeps;
	}
}