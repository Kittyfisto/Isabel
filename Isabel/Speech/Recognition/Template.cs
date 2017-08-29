using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	/// </summary>
	[DataContract]
	public sealed class Template
		: ITemplate
	{
		public Template()
		{
			VoiceCommands = new List<VoiceCommandTemplate>();
		}

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public List<VoiceCommandTemplate> VoiceCommands { get; set; }

		IReadOnlyList<VoiceCommandTemplate> ITemplate.VoiceCommands => VoiceCommands;
	}
}